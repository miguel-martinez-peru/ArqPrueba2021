using Institucional.Api.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sunedu.Transversal.Security.Auth.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CQRSSqlServer.Api.Controllers;

namespace Institucional.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/local")]
    [ApiController]
    public class LocalController : BaseController
    {
        private readonly IMediator _mediator;

        public LocalController(IMediator mediator, IConfiguration configuration) 
            : base(configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("")]
        public async Task<IActionResult> Insertar([FromBody] CrearLocalCommand command)
        {
            command.FechaCreacion = DateTime.Now;
            command.IpCreacion = IpCliente;

            command.EsEliminado = false;
            var cltToken = new System.Threading.CancellationToken();
            var commandResult = await _mediator.Send(command, cltToken);
            return commandResult.HasErrors ? (IActionResult)BadRequest(commandResult) : (IActionResult)Ok();
        }

    }
}