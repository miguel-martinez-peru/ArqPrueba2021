
namespace Soporte.Api.Infrastructure.Authorization
{
    public class AuthDataRequest
    {
        public string CODIGO_ACCION { get; set; }
        public string GUID_MENU { get; set; }
        public string GUID_ROL { get; set; }
        public string GUID_SESION { get; set; }
        public string GUID_SISTEMA { get; set; }
        public string IP_ORIGEN { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public int TIEMPO_TOKEN_ACTIVACION_DIAS { get; set; }
    }
}
