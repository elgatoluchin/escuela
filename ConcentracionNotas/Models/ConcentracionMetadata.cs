using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcentracionNotas.Models
{

    [MetadataType(typeof(ConcentracionMetadata))]
    public partial class Concentracion { }

    public class ConcentracionMetadata
    {
        [Display(Name = "Folio")]
        public int ConcentFolio { get; set; }

        [Display(Name = "ID")]
        public int AsignaturaId { get; set; }

        [Display(Name = "Alumno")]
        public int AlumnoId { get; set; }

        [Display(Name = "Promedio")]
        [Range(typeof(decimal), "1,0", "7,0")]
        public decimal ConcentPromedio { get; set; }

        [Display(Name = "Situacion")]
        public int ConcentSituacion { get; set; }

        [Display(Name = "Asistencia")]
        public int ConcentAsistencia { get; set; }
    }
}