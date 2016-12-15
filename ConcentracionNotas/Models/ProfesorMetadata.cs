using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConcentracionNotas.Models
{
    [MetadataType(typeof(ProfesorMetadata))]
    public partial class Profesor { }

    public class ProfesorMetadata
    {
        [Display(Name = "ID")]
        public int ProfesorId { get; set; }

        [Display(Name = "RUT")]
        public int ProfesorRut { get; set; }

        [Display(Name = "Digito")]
        public string ProfesorRutDigito { get; set; }

        [Required(ErrorMessage = "El nombre de la persona es requerido")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Ingrese un valor que se encuentre entre 2 a 25 caracteres de longitud")]
        [Display(Name = "Nombre")]
        public string ProfesorNombre { get; set; }

        [Required(ErrorMessage = "El apellido de la persona es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ingrese un valor que se encuentre entre 2 a 50 caracteres de longitud")]
        [Display(Name = "Apellido")]
        public string ProfesorApellido { get; set; }
    }
}