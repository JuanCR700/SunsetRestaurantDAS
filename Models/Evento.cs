using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunsetRestaurant.Models
{
    public class Evento
    {
        public int EventoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        // Relaciones foráneas
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Display(Name = "Tipo de Evento")]
        public int TipoEventoId { get; set; }

        [ForeignKey("TipoEventoId")]
        public TipoEvento TipoEvento { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha del Evento")]
        public DateTime? FechaEvento { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        [Display(Name = "Cantidad Invitados")]
        public int CantidadInvitados { get; set; }

        [DataType(DataType.Currency)]
        public decimal Adelanto { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Total a Pagar")]
        public decimal TotalAPagar { get; set; }
    }
}
