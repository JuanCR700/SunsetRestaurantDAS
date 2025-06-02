using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SunsetRestaurant.Data;
using SunsetRestaurant.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SunsetRestaurant.Controllers
{
    public class ColaboradorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColaboradorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Colaborador
        public async Task<IActionResult> Index()
        {
            return View(await _context.Colaboradores.ToListAsync());
        }

        // GET: Colaborador/Login
        public IActionResult Login()
        {
            return View();
        }

        // GET: Colaborador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var colaborador = await _context.Colaboradores
                .FirstOrDefaultAsync(m => m.ColaboradorId == id);
            if (colaborador == null) return NotFound();

            return View(colaborador);
        }

        // GET: Colaborador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colaborador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColabNombre,ColabEmail,ColabTelefono,ColabPassword")] Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(colaborador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(colaborador);
        }

        // GET: Colaborador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador == null) return NotFound();

            return View(colaborador);
        }

        // POST: Colaborador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColaboradorId,ColabNombre,ColabEmail,ColabTelefono,ColabPassword")] Colaborador colaborador)
        {
            if (id != colaborador.ColaboradorId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colaborador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColaboradorExists(colaborador.ColaboradorId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(colaborador);
        }

        // GET: Colaborador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var colaborador = await _context.Colaboradores
                .FirstOrDefaultAsync(m => m.ColaboradorId == id);
            if (colaborador == null) return NotFound();

            return View(colaborador);
        }

        // POST: Colaborador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colaborador = await _context.Colaboradores.FindAsync(id);
            _context.Colaboradores.Remove(colaborador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColaboradorExists(int id)
        {
            return _context.Colaboradores.Any(e => e.ColaboradorId == id);
        }
    }
}
