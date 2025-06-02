using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SunsetRestaurant.Data;
using SunsetRestaurant.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SunsetRestaurant.Controllers
{
    public class EventoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evento
        public async Task<IActionResult> Index()
        {
            var eventos = _context.Eventos
                .Include(e => e.Cliente)
                .Include(e => e.TipoEvento);
            return View(await eventos.ToListAsync());
        }

        // GET: Evento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var evento = await _context.Eventos
                .Include(e => e.Cliente)
                .Include(e => e.TipoEvento)
                .FirstOrDefaultAsync(m => m.EventoId == id);

            if (evento == null)
                return NotFound();

            return View(evento);
        }

        // GET: Evento/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre");
            ViewData["TipoEventoId"] = new SelectList(_context.TiposEvento, "TipoEventoId", "Nombre");
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", evento.ClienteId);
            ViewData["TipoEventoId"] = new SelectList(_context.TiposEvento, "TipoEventoId", "Nombre", evento.TipoEventoId);
            return View(evento);
        }

        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
                return NotFound();

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", evento.ClienteId);
            ViewData["TipoEventoId"] = new SelectList(_context.TiposEvento, "TipoEventoId", "Nombre", evento.TipoEventoId);
            return View(evento);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Evento evento)
        {
            if (id != evento.EventoId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.EventoId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", evento.ClienteId);
            ViewData["TipoEventoId"] = new SelectList(_context.TiposEvento, "TipoEventoId", "Nombre", evento.TipoEventoId);
            return View(evento);
        }

        // GET: Evento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var evento = await _context.Eventos
                .Include(e => e.Cliente)
                .Include(e => e.TipoEvento)
                .FirstOrDefaultAsync(m => m.EventoId == id);

            if (evento == null)
                return NotFound();

            return View(evento);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoId == id);
        }
    }
}
