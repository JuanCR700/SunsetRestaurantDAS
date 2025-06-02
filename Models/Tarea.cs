using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SunsetRestaurant.Models
{
    public class Tarea
    {
        public int TareaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public int? AsignadoAId { get; set; }
        [ForeignKey("AsignadoAId")]
        public Colaborador AsignadoA { get; set; }

        [Required]
        public string EstadoTarea { get; set; } // Nueva propiedad
    }



}
