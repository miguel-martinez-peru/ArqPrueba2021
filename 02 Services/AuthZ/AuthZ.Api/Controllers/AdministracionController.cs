using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AuthZ.Api.Application.Commands;
using AuthZ.Api.Application.Queries.Permiso;
using AuthZ.Api.Application.Queries.RolPermiso;
using AuthZ.Api.Application.ViewModels;
using AuthZ.Api.Application.ViewModels.PermisoViewModel;
using AuthZ.Api.Application.ViewModels.RolPermisoViewModel;
using AuthZ.Api.Model;
using AuthZ.Cliente;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sunedu.Transversal.Security.Auth.Filters;

namespace AuthZ.Api.Controllers
{
    [ApiController]
    [Authorize]
    //[Route("[controller]")]
    public class AdministracionController : BaseController
    {
        private readonly ILogger<AuthZController> _logger;
        private readonly IMediator _mediator;
        private readonly IPermisoQueries _permisoQueries;
        private readonly IRolPermisoQueries _rolPermisoQueries;
        //private TestPermisos _permisosProvider;

        public AdministracionController(ILogger<AuthZController> logger,
            IMediator mediator,
            IPermisoQueries permisoQueries,
            IRolPermisoQueries rolPermisoQueries)
        {
            _logger = logger;
            _mediator = mediator;
            _permisoQueries = permisoQueries;
            _rolPermisoQueries = rolPermisoQueries;
        }

        private Guid GetUserID { get { return new Guid(UserSesion.FindFirst("guid_usuario").Value); } }

        #region permiso

        [Route("permiso")]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        [HttpPost]
        public async Task<IActionResult> RegistrarPermiso([FromBody] CrearPermisoCommand command)
        {
            command.UsuarioCreacion = new Guid("254d14ac-1843-405d-b470-d75d71343c15");   // this.GetUserID;
            command.FechaCreacion = DateTime.Now;
            command.IpCreacion = IpCliente;
            command.EsActivo = true;

            var cltToken = new System.Threading.CancellationToken();
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasError() ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok(commandResult);
        }

        [Route("permiso")]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        [HttpDelete]
        public async Task<IActionResult> EliminarPermiso([FromQuery] int idPermiso)
        {
            var command = new EliminarPermisoCommand { IdPermiso = idPermiso };
            command.UsuarioCreacion = this.GetUserID;
            command.FechaCreacion = DateTime.Now;
            command.IpCreacion = IpCliente;

            var cltToken = new System.Threading.CancellationToken();
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasError() ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok();
        }

        [Route("permiso")]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        [HttpPut]
        public async Task<IActionResult> EditarPermiso([FromBody] EditarPermisoCommand command)
        {
            command.UsuarioCreacion = this.GetUserID;
            command.FechaCreacion = DateTime.Now;
            command.IpCreacion = IpCliente;

            var cltToken = new System.Threading.CancellationToken();
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasError() ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok();

        }

        [Route("permiso")]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsRequestViewModel<PermisoResponseViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> BuscarPermiso([FromBody] PaginatedItemsRequestViewModel<PermisoRequestViewModel> request)
        {
            var filialLista = await _permisoQueries.Buscar(request);
            return Ok(filialLista);
        }

        #endregion


        #region rolpermiso

        [Route("rolpermiso")]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        [HttpPost]
        public async Task<IActionResult> RegistrarRolPermiso([FromBody] CrearRolPermisoCommand command)
        {
            command.UsuarioCreacion = new Guid("254d14ac-1843-405d-b470-d75d71343c15");   // this.GetUserID;
            command.FechaCreacion = DateTime.Now;
            command.IpCreacion = IpCliente;

            var cltToken = new System.Threading.CancellationToken();
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasError() ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok(commandResult);
        }

        [Route("rolpermiso")]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        [HttpDelete]
        public async Task<IActionResult> EliminarRolPermiso([FromQuery] EliminarRolPermisoCommand command)
        {
            command.UsuarioCreacion = this.GetUserID;
            command.FechaCreacion = DateTime.Now;
            command.IpCreacion = IpCliente;

            var cltToken = new System.Threading.CancellationToken();
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasError() ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok();
        }

        [Route("rolpermiso")]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        [HttpPut]
        public async Task<IActionResult> EditarRolPermiso([FromBody] EditarRolPermisoCommand command)
        {
            command.UsuarioCreacion = this.GetUserID;
            command.FechaCreacion = DateTime.Now;
            command.IpCreacion = IpCliente;

            var cltToken = new System.Threading.CancellationToken();
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasError() ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok();

        }

        [Route("rolpermiso")]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsRequestViewModel<RolPermisoResponseViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> BuscarRolPermiso([FromQuery] PaginatedItemsRequestViewModel<RolPermisoRequestViewModel> request)
        {
            var filialLista = await _rolPermisoQueries.Buscar(request);
            return Ok(filialLista);
        }

        #endregion

        //[Route("RegistrarPermiso")]
        //[HttpPost]
        //[Puede(PermisosEnum.Cache1)]
        //public IActionResult RegistrarPermiso(ValidaRequest param)
        //{
        //    //esto se debe obtener desde la bd y estar cacheado
        //    var permisosProvider = TestPermisos.Permisos.FirstOrDefault(x => x.SubjectId == param.sub);
        //    if(permisosProvider != null)
        //    {
        //        if (permisosProvider.Roles.Any(x => x.Permisos.Any(x => x.ToString() == param.permiso)))
        //            return Ok();
        //    }

        //    return Forbid();
        //}

        //[Route("AsignarPermiso")]
        //[HttpPost]
        //[Puede(PermisosEnum.Cache1)]
        //public IActionResult AsignarPermiso(ValidaRequest param)
        //{
        //    //esto se debe obtener desde la bd y estar cacheado
        //    var permisosProvider = TestPermisos.Permisos.FirstOrDefault(x => x.SubjectId == param.sub);
        //    if (permisosProvider != null)
        //    {
        //        if (permisosProvider.Roles.Any(x => x.Permisos.Any(x => x.ToString() == param.permiso)))
        //            return Ok();
        //    }

        //    return Forbid();
        //}

    }
}
