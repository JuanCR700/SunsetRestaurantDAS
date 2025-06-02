using System;
using System.ComponentModel.DataAnnotations;

namespace SunsetRestaurant.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int? ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }
    }
}
