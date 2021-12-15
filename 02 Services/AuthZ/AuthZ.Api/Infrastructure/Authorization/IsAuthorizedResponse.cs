using System.Collections.Generic;

namespace Soporte.Api.Infrastructure.Authorization
{
    public class IsAuthorizedResponse
    {
        public string GUID_USUARIO { get; set; }

        public bool IS_AUTHORIZED { get; set; }
        public bool HasErrors { get; set; }
        public List<GenericMessage> Messages { get; set; }
    }
    public class GenericMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
    public class UsuarioResponse
    {
        public string APELLIDO_MATERNO { get; set; }

        public string APELLIDO_PATERNO { get; set; }

        public string CORREO_ELECTRONICO { get; set; }

        public object FECHA_NACIMIENTO { get; set; }

        public string GUID_USUARIO { get; set; }

        public string NOMBRES { get; set; }

        public string NUMERO_DOCUMENTO { get; set; }

        public string TIPO_DOCUMENTO { get; set; }
    }
    public class MenuResponse
    {
        public string GUID { get; set; }
        public string NOMBRE { get; set; }
        public string GUID_PADRE { get; set; }
        public string ID_CONTROL { get; set; }
    }
    public class Response
    {
        public MenuResponse[] MENU { get; set; }
    }
}
