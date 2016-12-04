using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inacap.Dominio.Nucleo
{
    public class ValidarObjetoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var resultados = new List<ValidationResult>();
            var contexto = new ValidationContext(value, null, null);

            Validator.TryValidateObject(value, contexto, resultados, true);

            if (resultados.Count != 0)
            {
                var resultadosCompuestos = new ValidarResultadosCompuestos(string.Format("{0}", validationContext.DisplayName));
                resultados.ForEach(resultadosCompuestos.AgregarResultado);

                return resultadosCompuestos;
            }

            return ValidationResult.Success;
        }
    }
}
