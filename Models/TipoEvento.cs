using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SunsetRestaurant.Models
{
    public class TipoEvento
    {
        [Key]
        public int TipoEventoId { get; set; }

        [Required]
        public string Nombre { get; set; }

        // Relación 1 a muchos con Evento
        public ICollection<Evento> Eventos { get; set; }
    }
}
