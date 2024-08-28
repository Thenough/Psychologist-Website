using Core.Models.Concrete;
using Core.Services;
using Core.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PsikoWeb.WebApp.Models;
using Repository;
using System.Data;

namespace PsikoWeb.WebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly UserManager<AppUser> _userManager;
        public EventController(IBookingService bookingService, UserManager<AppUser> userManager)
        {
            _bookingService = bookingService;
            _userManager = userManager;
        }
        public async Task <IActionResult> Room(int id, string starts = null)
        {
            // get logged in user
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

           
            if (starts == null)
            {
                
                starts = DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToString("yyyy-MM-dd");
            }

           
            var events = _bookingService.GetUserEventsForRoom(id);

            var vm = new EventViewModel { UserId = userId!, RoomId = id, Start = starts, End = starts, Events = events };
            return View(vm);

        }
        [HttpGet]
        public async Task<IActionResult> Add(int id, DateTime start, DateTime end)
        {
            // Kullanıcıyı bul
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound(); 
            }

            // Odayı al
            var room = _bookingService.GetRoom(id);
            if (room == null)
            {
                return NotFound(); 
            }

           
            var e = new Event
            {
                RoomId = room.Id,
                UserId = user.Id,
                Start = start,
                End = end
            };

         
            var v = EventViewModel.FromEvent(e);

           
            return View(v);
        }

        [HttpPost]
        public IActionResult Add([Bind("RoomId", "UserId", "Start", "End")] EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
               
                var newEvent = vm.ToEvent();

            
                var updatedEvent = _bookingService.AddEvent(newEvent);

               
                return RedirectToAction("Index"); 
            }

           
            return View(vm);
        }


        public async Task<IActionResult> Edit(int id)
        {
          
            var user = await _userManager.FindByIdAsync(id.ToString());

           
            var e = _bookingService.GetEvent(id);

            var vm = EventViewModel.FromEvent(e);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind] EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var updated = _bookingService.UpdateEvent(vm.ToEvent());
                if (updated != null)
                {

                }
                else
                {
                }
            }
            return View(vm);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var e = _bookingService.GetEvent(id);
            if (e == null)
            {

            }

            if (_bookingService.DeleteEvent(id))
            {
               
            }
            else
            {
                
            }
            return RedirectToAction(nameof(Room), new { Id = e.RoomId });
        }

      
        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateDate([BindRequired, FromQuery] EventViewModel vm)
        {
            if (!_bookingService.IsValidEvent(vm.ToEvent()))
            {
                return Json($"Event overlaps another event.");
            }

            return Json(true);
        }
    }
}
