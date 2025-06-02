using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SunsetRestaurant.Data;
using SunsetRestaurant.Models;
using System.Linq;

namespace SunsetRestaurant.Controllers
{
    public class EventosClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Registrar nuevo evento
        [HttpGet]
        public IActionResult Registrar()
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            if (clienteId == null)
                return RedirectToAction("Login", "Clientes");

            return View();
        }

        // POST: Registrar evento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(EventoCliente evento)
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            if (clienteId == null)
                return RedirectToAction("Login", "Clientes");

            evento.ClienteId = (int)clienteId;

            if (ModelState.IsValid)
            {
                _context.EventosClientes.Add(evento);
                _context.SaveChanges();
                return RedirectToAction("MisReservas");
            }

            return View(evento);
        }

        // GET: Ver eventos del cliente
        [HttpGet]
        public IActionResult MisReservas()
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            if (clienteId == null)
                return RedirectToAction("Login", "Clientes");

            var eventos = _context.EventosClientes
                .Where(e => e.ClienteId == clienteId)
                .ToList();

            return View(eventos);
        }

        // GET: Editar
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            if (clienteId == null)
                return RedirectToAction("Login", "Clientes");

            var evento = _context.EventosClientes.FirstOrDefault(e => e.Id == id && e.ClienteId == clienteId);
            if (evento == null)
                return NotFound();

            return View(evento);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(EventoCliente evento)
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            if (clienteId == null || evento.ClienteId != clienteId)
                return RedirectToAction("Login", "Clientes");

            if (ModelState.IsValid)
            {
                _context.EventosClientes.Update(evento);
                _context.SaveChanges();
                return RedirectToAction("MisReservas");
            }

            return View(evento);
        }

        // GET: Eliminar
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            if (clienteId == null)
                return RedirectToAction("Login", "Clientes");

            var evento = _context.EventosClientes.FirstOrDefault(e => e.Id == id && e.ClienteId == clienteId);
            if (evento != null)
            {
                _context.EventosClientes.Remove(evento);
                _context.SaveChanges();
            }

            return RedirectToAction("MisReservas");
        }
    }
}
