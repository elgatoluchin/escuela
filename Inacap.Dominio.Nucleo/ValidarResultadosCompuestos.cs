using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inacap.Dominio.Nucleo
{
    public class ValidarResultadosCompuestos : ValidationResult
    {
        private readonly List<ValidationResult> _resultados = new List<ValidationResult>();

        public IEnumerable<ValidationResult> Resultados
        {
            get { return _resultados; }
        }

        public ValidarResultadosCompuestos(string errorMessage) : base(errorMessage) { }

        public ValidarResultadosCompuestos(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }

        protected ValidarResultadosCompuestos(ValidationResult validationResult) : base(validationResult) { }

        public void AgregarResultado(ValidationResult resultadoValidacion)
        {
            _resultados.Add(resultadoValidacion);
        }
    }
}
