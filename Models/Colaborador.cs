using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SunsetRestaurant.Models
{
    public class Colaborador
    {
        [Key]
        public int ColaboradorId { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [Display(Name = "Nombre Completo")]
        public string ColabNombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido")]
        [Display(Name = "Correo Electrónico")]
        public string ColabEmail { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Display(Name = "Teléfono")]
        public string ColabTelefono { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string ColabPassword { get; set; }

        // Nota: Este campo "Nombre" está en tu BD, pero parece redundante. Puedes mapearlo si lo usas:
        public string? Nombre { get; set; }  // Opcional
    }
}
