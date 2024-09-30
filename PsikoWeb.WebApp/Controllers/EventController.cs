using Core.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace PsikoWeb.WebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public EventController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Event.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Event evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Randevu başarıyla oluşturuldu";
                return RedirectToAction("Index");
            }
            return View(evento);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound(); ;
            }

            var evento = await _context.Event.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);

        }

        [HttpPost]
        public async Task<IActionResult> Update(Event evento)
        {
            if (ModelState.IsValid)
            {
                var eventoEncontrado = await _context.Event.FindAsync(evento.Id);
                if (eventoEncontrado == null)
                {
                    return NotFound();
                }

                eventoEncontrado.Title = evento.Title;
                eventoEncontrado.Date = evento.Date;
                eventoEncontrado.Description = evento.Description;

                _context.Update(eventoEncontrado);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Randevu başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            return View(evento);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var evento = await _context.Event.FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            _context.Event.Remove(evento);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Randevu başarıyla silindi";
            return RedirectToAction("Index");
        }
    }
}
