using Microsoft.EntityFrameworkCore;
using SunsetRestaurant.Models;

namespace SunsetRestaurant.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<TipoEvento> TiposEvento { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<EventoCliente> EventosClientes { get; set; }
    }
}
