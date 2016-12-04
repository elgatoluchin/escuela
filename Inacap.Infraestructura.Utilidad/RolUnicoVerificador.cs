using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Inacap.Infraestructura.Utilidad
{
    public class RolUnicoVerificador
    {
        readonly RolUnicoNacional _rolUnico;

        protected readonly IList<string> ValidacionErrores = new List<string>();

        public RolUnicoVerificador(RolUnicoNacional rolUnico)
        {
            _rolUnico = rolUnico;

            if (string.IsNullOrEmpty(rolUnico.Rut) || rolUnico.Rut.Length < 3)
                ValidacionErrores.Add("Rol Único no puede estar vacío, o ser menor a tres caracteres");

            if (EsValido() && Extraer()) Validar();
        }

        bool Extraer()
        {
            try
            {
                int numero;

                Regex regex = new Regex("[\\~#%&*{}/:.,<>?|\"-]");

                // Eliminamos todos los caracteres inválidos
                string caracteresInvalidos = regex.Replace(_rolUnico.Rut, "");

                string rut = Regex.Replace(caracteresInvalidos, @"\s+", "");

                rut = rut.ToUpper();

                int.TryParse(rut.Substring(0, rut.Length - 1), out numero);

                _rolUnico.Numero = numero;
                _rolUnico.DigitoVerificador = rut.Substring(rut.Length - 1);
            }
            catch (Exception ex)
            {
                ValidacionErrores.Add(string.Format("Error inesperado: {0}", ex.Message));
                return false;
            }

            return true;
        }

        bool Validar()
        {
            if (!Regex.IsMatch(_rolUnico.DigitoVerificador, "[0-9]+|[(K)]"))
                ValidacionErrores.Add(string.Format("Digito verificador '{0}' no es valido", _rolUnico.DigitoVerificador));

            int? numero = _rolUnico.Numero, s = 1;

            for (int m = 0; numero != 0; numero /= 10)
                s = (s + numero % 10 * (9 - m++ % 6)) % 11;

            s = (s > 0) ? (s + 47) : 75;

            if (Convert.ToChar(s).ToString() != _rolUnico.DigitoVerificador.ToString())
                ValidacionErrores.Add(string.Format("Rol Único '{0}-{1}' no es valido", _rolUnico.Numero, _rolUnico.DigitoVerificador));

            if (!EsValido()) return false;
            return true;
        }

        public IList<string> ObtenerErrores()
        {
            return ValidacionErrores;
        }

        public bool EsValido()
        {
            return 0 == ValidacionErrores.Count;
        }

        public RolUnicoNacional ObtenerRolUnico()
        {
            return _rolUnico;
        }
    }
}
