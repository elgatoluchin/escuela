using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcentracionNotas.Models
{
    [MetadataType(typeof (AsignaturaMetadata))]
    public partial class Asignatura {}

    public class AsignaturaMetadata
    {
        [Display(Name = "ID")]
        public int AsignaturaId { get; set; }

        [Display(Name = "Profesor ID")]
        [Required(ErrorMessage = "El campo es requerido.")]
        public int ProfesorId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo es requerido.")]
        public string AsignaturaNombre { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo es requerido.")]
        public int AsignaturaTipo { get; set; }
    }
}