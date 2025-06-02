using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SunsetRestaurant.Data;
using SunsetRestaurant.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SunsetRestaurant.Controllers
{
    public class TareaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TareaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tarea
        public async Task<IActionResult> Index()
        {
            var tareas = _context.Tareas
                .Include(t => t.Evento)
                .Include(t => t.AsignadoA);
            return View(await tareas.ToListAsync());
        }

        // GET: Tarea/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tarea = await _context.Tareas
                .Include(t => t.Evento)
                .Include(t => t.AsignadoA)
                .FirstOrDefaultAsync(m => m.TareaId == id);

            if (tarea == null) return NotFound();

            return View(tarea);
        }

        // GET: Tarea/Create
        public IActionResult Create()
        {
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Nombre");
            ViewData["AsignadoAId"] = new SelectList(_context.Colaboradores, "ColaboradorId", "ColabNombre");
            return View();
        }

        // POST: Tarea/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TareaId,Nombre,EventoId,AsignadoAId")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Nombre", tarea.EventoId);
            ViewData["AsignadoAId"] = new SelectList(_context.Colaboradores, "ColaboradorId", "ColabNombre", tarea.AsignadoAId);
            return View(tarea);
        }

        // GET: Tarea/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();

            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Nombre", tarea.EventoId);
            ViewData["AsignadoAId"] = new SelectList(_context.Colaboradores, "ColaboradorId", "ColabNombre", tarea.AsignadoAId);
            return View(tarea);
        }

        // POST: Tarea/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TareaId,Nombre,EventoId,AsignadoAId")] Tarea tarea)
        {
            if (id != tarea.TareaId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.TareaId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Nombre", tarea.EventoId);
            ViewData["AsignadoAId"] = new SelectList(_context.Colaboradores, "ColaboradorId", "ColabNombre", tarea.AsignadoAId);
            return View(tarea);
        }

        // GET: Tarea/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tarea = await _context.Tareas
                .Include(t => t.Evento)
                .Include(t => t.AsignadoA)
                .FirstOrDefaultAsync(m => m.TareaId == id);

            if (tarea == null) return NotFound();

            return View(tarea);
        }

        // POST: Tarea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.TareaId == id);
        }
    }
}
