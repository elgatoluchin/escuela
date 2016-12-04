using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Inacap.Dominio.Nucleo
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RolUnicoAttribute : ValidationAttribute
    {
        private readonly IList<string> _validacionErrores = new List<string>();
        private string _identificacion;
        private string _digitoVerificador;
        private int _numero;

        public override bool IsValid(object value)
        {
            var resultado = true;

            if (null != value)
            {
                _identificacion = Convert.ToString(value).Trim();
                _digitoVerificador = string.Empty;
                _numero = -1;
                _validacionErrores.Clear();

                if (string.IsNullOrEmpty(_identificacion) || _identificacion.Length < 3)
                {
                    ErrorMessage = "Rol Único no puede estar vacío, o ser menor a tres caracteres";
                    resultado = false;
                }

                if (resultado && !Extraer())
                {
                    ErrorMessage = _validacionErrores.ElementAt(0);
                    resultado = false;
                }

                if (resultado && !Validar())
                {
                    ErrorMessage = _validacionErrores.ElementAt(0);
                    resultado = false;
                }
            }

            return resultado;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (IsValid(value))
           ? ValidationResult.Success
           : new ValidationResult(ErrorMessage);
        }

        private bool Extraer()
        {
            try
            {
                int parteNumerica;

                Regex regex = new Regex("[\\~#%&*{}/:.,<>?|\"-]");

                // Eliminamos todos los caracteres inválidos
                string caracteresInvalidos = regex.Replace(_identificacion, "");

                string rut = Regex.Replace(caracteresInvalidos, @"\s+", "");

                rut = rut.ToUpper();

                int.TryParse(rut.Substring(0, rut.Length - 1), out parteNumerica);

                _numero = parteNumerica;
                _digitoVerificador = rut.Substring(rut.Length - 1);
            }
            catch (Exception ex)
            {
                _validacionErrores.Add(string.Format("Error inesperado: {0}", ex.Message));
                return false;
            }

            return true;
        }

        private bool Validar()
        {
            if (!Regex.IsMatch(_digitoVerificador, "[0-9]+|[(K)]"))
                _validacionErrores.Add(string.Format("Digito verificador '{0}' no es valido", _digitoVerificador));

            int? parteNumerica = _numero, s = 1;

            for (int m = 0; parteNumerica != 0; parteNumerica /= 10)
                s = (s + parteNumerica%10*(9 - m++%6))%11;

            s = (s > 0) ? (s + 47) : 75;

            if (Convert.ToChar(s).ToString() != _digitoVerificador.ToString())
                _validacionErrores.Add(string.Format("Rol Único '{0}-{1}' no es valido", _numero, _digitoVerificador));

            if (!EsValido()) return false;
            return true;
        }

        private bool EsValido()
        {
            return 0 == _validacionErrores.Count;
        }
    }
}
