using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcentracionNotas.Models
{
    [MetadataType(typeof(NotaMetadata))]
    public partial class Nota { }

    public class NotaMetadata
    {
        [Display(Name = "ID")]
        public int NotaId { get; set; }

        [Display(Name = "Folio")]
        public int ConcentFolio { get; set; }

        [Display(Name = "Número")]
        public int NotaNumero { get; set; }

        [Display(Name = "Ponderacion")]
        public int NotaPonderacion { get; set; }

        [Display(Name = "Obtenido")]
        [Range(typeof(decimal), "1,0", "7,0")]
        public decimal NotaObtenido { get; set; }
    }
}