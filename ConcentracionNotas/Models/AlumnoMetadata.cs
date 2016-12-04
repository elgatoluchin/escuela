using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcentracionNotas.Models
{
    [MetadataType(typeof(AlumnoMetadata))]
    public partial class Alumno {}

    public class AlumnoMetadata
    {
        [Display(Name = "ID")]
        public int AlumnoId { get; set; }

        [Display(Name = "RUT")]
        public int AlumnoRut { get; set; }

        [Display(Name = "Digito")]
        public string AlumnoRutDigito { get; set; }

        [Required(ErrorMessage = "El nombre de la persona es requerido")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Ingrese un valor que se encuentre entre 2 a 25 caracteres de longitud")]
        [Display(Name = "Nombre")]
        public string AlumnoNombre { get; set; }

        [Required(ErrorMessage = "El apellido de la persona es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ingrese un valor que se encuentre entre 2 a 50 caracteres de longitud")]
        [Display(Name = "Apellido")]
        public string AlumnoApellido { get; set; }
    }
}