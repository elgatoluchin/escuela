using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Inacap.Dominio.Nucleo;

namespace ConcentracionNotas.Models
{
    public class ProfesorModelo : Profesor
    {
        [Required(ErrorMessage = "El campo RUT del trabajador es requerido.")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "El RUT del trabajador debe tener una longitud de 3 a 12 caracteres.")]
        [RolUnico()]
        [Display(Name = "Rut")]
        public string RolUnico { get; set; }
    }
}