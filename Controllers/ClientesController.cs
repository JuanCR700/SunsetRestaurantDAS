using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SunsetRestaurant.Data;
using SunsetRestaurant.Models;
using System.Linq;

namespace SunsetRestaurant.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Opciones de ingreso
        [HttpGet]
        public IActionResult OpcionesIngreso()
        {
            return View();
        }

        // ✅ Vista Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ✅ Proceso Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Email, string Password)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Email == Email && c.Password == Password);

            if (cliente != null)
            {
                HttpContext.Session.SetInt32("ClienteId", cliente.ClienteId);
                HttpContext.Session.SetString("ClienteNombre", cliente.Nombre);

                return RedirectToAction("Registrar", "EventosClientes");
            }

            ViewBag.Error = "Correo o contraseña incorrectos.";
            return View();
        }

        // ✅ Vista Registro
        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }

        // ✅ Proceso Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrarse(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(cliente);
        }

        // ✅ Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
