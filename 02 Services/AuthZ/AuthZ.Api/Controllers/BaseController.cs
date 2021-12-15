using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthZ.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public ClaimsPrincipal UserSesion { get { return (ClaimsPrincipal)User; } }
        public string IpCliente
        {
            get
            {
                if (Request.Headers.ContainsKey("IpClient"))
                {
                    return Request.Headers["IpClient"].ToString();
                }
                return HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }

        //protected RequestHeadersViewModel GetRequestHeader()
        //{
        //    var requestHeaders = new RequestHeadersViewModel();

        //    if (!string.IsNullOrEmpty(Request.Headers["guid_sistema"].ToString()))
        //        requestHeaders.guidSistema = new Guid(Request.Headers["guid_sistema"]);

        //    if (!string.IsNullOrEmpty(Request.Headers["guid_modulo"].ToString()))
        //        requestHeaders.guidModulo = new Guid(Request.Headers["guid_modulo"]);

        //    if (!string.IsNullOrEmpty(Request.Headers["guid_menu"].ToString()))
        //        requestHeaders.guidOpcion = new Guid(Request.Headers["guid_menu"]);

        //    if (!string.IsNullOrEmpty(Request.Headers["guid_rol"].ToString()))
        //        requestHeaders.guidRol = new Guid(Request.Headers["guid_rol"]);

        //    return requestHeaders;
        //}

        protected static internal class ACCIONES
        {
            public static string CREAR { get; } = "CRE";
            public static string EDITAR { get; } = "EDT";
            public static string ELIMINAR { get; } = "ELM";
            public static string CONSULTAR { get; } = "CON";
        }

    }
}
