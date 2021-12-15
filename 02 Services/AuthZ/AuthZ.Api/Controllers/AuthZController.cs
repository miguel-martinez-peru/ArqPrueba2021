using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthZ.Api.Application.Queries.RolPermiso;
using AuthZ.Api.Model;
using AuthZ.Cliente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthZ.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthZController : ControllerBase
    {
        private readonly ILogger<AuthZController> _logger;
        //private TestPermisos _permisosProvider;
        private readonly IRolPermisoQueries _rolPermisoQueries;

        public AuthZController(ILogger<AuthZController> logger,
            IRolPermisoQueries rolPermisoQueries)
        {
            _logger = logger;
            _rolPermisoQueries = rolPermisoQueries;
        }
        
        [Route("Valida")]
        [HttpPost]
        public IActionResult Valida(ValidaRequest param)
        {
            //esto se debe obtener desde la bd y estar cacheado
            var valido = _rolPermisoQueries.ValidaPermiso(new Aplication.ViewModels.ValidaRequestViewModel { permiso = param.permiso, rol = param.sub });
            if (valido)
                return Ok();

            return Forbid();
        }

    }
}
