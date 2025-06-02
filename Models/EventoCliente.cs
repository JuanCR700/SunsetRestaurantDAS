using System;
using System.ComponentModel.DataAnnotations;

namespace SunsetRestaurant.Models
{
    public class EventoCliente
    {
        public int Id { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CantidadInvitados { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Presupuesto { get; set; }

        public int ClienteId { get; set; }
    }
}
