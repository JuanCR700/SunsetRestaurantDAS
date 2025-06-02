using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SunsetRestaurant.Data;
using SunsetRestaurant.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SunsetRestaurant.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reservas = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Colaborador)
                .ToListAsync();

            return View(reservas);
        }

        public IActionResult Create()
        {
            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.Colaboradores = _context.Colaboradores.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Reservas.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.Colaboradores = _context.Colaboradores.ToList();
            return View(reserva);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.Colaboradores = _context.Colaboradores.ToList();
            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Reservas.Update(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.Colaboradores = _context.Colaboradores.ToList();
            return View(reserva);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Colaborador)
                .FirstOrDefaultAsync(r => r.ReservaId == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Colaborador)
                .FirstOrDefaultAsync(r => r.ReservaId == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }
    }
}
