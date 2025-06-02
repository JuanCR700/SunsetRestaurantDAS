using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SunsetRestaurant.Data;
using SunsetRestaurant.Models;
using System.Threading.Tasks;
using System.Linq;

namespace SunsetRestaurant.Controllers
{
    public class TipoEventoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoEventoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoEvento
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposEvento.ToListAsync());
        }

        // GET: TipoEvento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tipoEvento = await _context.TiposEvento
                .FirstOrDefaultAsync(m => m.TipoEventoId == id);

            if (tipoEvento == null) return NotFound();

            return View(tipoEvento);
        }

        // GET: TipoEvento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoEventoId,Nombre")] TipoEvento tipoEvento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoEvento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEvento);
        }

        // GET: TipoEvento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tipoEvento = await _context.TiposEvento.FindAsync(id);
            if (tipoEvento == null) return NotFound();

            return View(tipoEvento);
        }

        // POST: TipoEvento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoEventoId,Nombre")] TipoEvento tipoEvento)
        {
            if (id != tipoEvento.TipoEventoId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoEvento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEventoExists(tipoEvento.TipoEventoId))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEvento);
        }

        // GET: TipoEvento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tipoEvento = await _context.TiposEvento
                .FirstOrDefaultAsync(m => m.TipoEventoId == id);
            if (tipoEvento == null) return NotFound();

            return View(tipoEvento);
        }

        // POST: TipoEvento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoEvento = await _context.TiposEvento.FindAsync(id);
            _context.TiposEvento.Remove(tipoEvento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEventoExists(int id)
        {
            return _context.TiposEvento.Any(e => e.TipoEventoId == id);
        }
    }
}
