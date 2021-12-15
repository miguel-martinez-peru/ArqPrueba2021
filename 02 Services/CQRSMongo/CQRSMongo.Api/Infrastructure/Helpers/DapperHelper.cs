using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Infrastructure.Helpers
{
    public class DapperHelper
    {


        public static string ObtenValoresInt(string param)
        {
            //divisor es la coma
            //si no todos los elementos con int, genera error


            param = Uri.EscapeUriString(param).Replace("'", "''");

            bool esValido = true;
            var elementos = param.Split(",").ToList();

            elementos.ForEach(x => {
                int convertido;
                if (!(int.TryParse(x, out convertido)))
                    esValido = false;
            });

            if (!esValido)
                throw new Exception();

            var concatenado = "";

            elementos.ForEach(x => {

                var elem = int.Parse(x);
                concatenado += string.Format("{0},", elem);
            });
            concatenado = concatenado.Substring(0, concatenado.Length - 1);


            return concatenado;
        }

        public static string ObtenValoresCadena(string param)
        {
            param = Uri.EscapeUriString(param).Replace("'", "''");

            var elementos = param.Split(",").ToList();

            var concatenado = "";
            elementos.ForEach(x => {
                concatenado += string.Format("\'{0}\',", x);
            });
            concatenado = concatenado.Substring(0, concatenado.Length - 1);


            return concatenado;
        }
    }
}
