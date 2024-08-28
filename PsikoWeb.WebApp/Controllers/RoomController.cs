using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace PsikoWeb.WebApp.Controllers
{
    public class RoomController : Controller
    {
        private readonly IBookingService _bookingService;
        public RoomController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public IActionResult Index()
        {
            var rooms = _bookingService.GetRooms();
            return View(rooms);
        }
    }
}
