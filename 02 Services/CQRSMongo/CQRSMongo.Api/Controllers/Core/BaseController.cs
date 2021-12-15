using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AcademicoOds.Api.Controllers
{
    public enum EnmAccionRegistro
    {
        Nuevo = 1,
        Edicion = 2,
        Consulta = 3
    }

    public class BaseController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
    }
}
