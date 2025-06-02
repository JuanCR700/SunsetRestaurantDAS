using Microsoft.AspNetCore.Mvc;
using SunsetRestaurant.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SunsetRestaurant.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // VISTA PRINCIPAL DEL DASHBOARD
        public IActionResult Index()
        {
            return View();
        }

        // VISTA PARA LAS TAREAS
        public async Task<IActionResult> Tareas()
        {
            var tareas = await _context.Tareas
                                       .Include(t => t.Evento)
                                       .ToListAsync();
            return View(tareas);
        }

        // VISTA PARA VER REGISTROS DE EVENTOS
        public async Task<IActionResult> Registros()
        {
            var eventos = await _context.Eventos.ToListAsync();
            return View(eventos);
        }

        // REDIRECCIÓN PARA MODIFICAR EVENTO
        public IActionResult ModificarEvento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Edit", "Eventos", new { id = id });
        }

        // VISTA DEL CALENDARIO
        public async Task<IActionResult> Calendario()
        {
            var eventos = await _context.Eventos
                .Select(e => new
                {
                    title = $"Evento #{e.EventoId}",
                    start = e.FechaEvento != null
                        ? e.FechaEvento.Value.ToString("yyyy-MM-dd")
                        : DateTime.MinValue.ToString("yyyy-MM-dd") // o manejar como null si es necesario
                })
                .ToListAsync();

            ViewBag.Eventos = eventos;
            return View();
        }

        // VISTA DE LOGIN PARA COLABORADORES
        public IActionResult Login()
        {
            return View();
        }

        // PROCESO DE LOGIN (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string usuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                ViewBag.Error = "Debe ingresar usuario y contraseña.";
                return View();
            }

            if (!int.TryParse(usuario, out int usuarioId))
            {
                ViewBag.Error = "El usuario debe ser un número válido (ID).";
                return View();
            }

            // Hasheo básico. ⚠️ Reemplazar con BCrypt o similar para producción.
            string hashContrasena = HashPassword(contrasena);

            var colaborador = await _context.Colaboradores
                .FirstOrDefaultAsync(c => c.ColaboradorId == usuarioId && c.ColabPassword == hashContrasena);

            if (colaborador != null)
            {
                // Aquí podrías implementar Claims, Cookie Auth, etc.
                TempData["ColabNombre"] = colaborador.ColabNombre;
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Credenciales incorrectas.";
            return View();
        }

        // Hasheo simple (⚠️ NO USAR en producción; usar BCrypt o similar)
        private string HashPassword(string password)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower(); // .NET 5+
            }
        }
    }
}
