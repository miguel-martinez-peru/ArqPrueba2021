using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AcademicoOds.Api.Application.Queries;
using AcademicoOds.Api.Application.ViewModels;
using AcademicoOds.Api.Application.ViewModels.IngresanteModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Sunedu.Core;
using Sunedu.Transversal.Security.Auth.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AcademicoOds.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/v1/ingresante")]
    [ApiController]
    public class IngresanteController : BaseController
    {
        private readonly IIngresanteQueries _ingresanteQueries;
        private readonly ILogger<IngresanteController> logger;

        public IngresanteController(IIngresanteQueries ingresanteQueries
            , IConfiguration configuration
            , ILogger<IngresanteController> logger) : base(configuration)
        {
            _ingresanteQueries = ingresanteQueries ?? throw new ArgumentNullException(nameof(ingresanteQueries));
            this.logger = logger;
        }


        /// <summary>
        /// Permite consultar los datos de los ingresantes desde el Academico ODS
        /// </summary>
        /// <response code="200">Devuelve la lista de resultados de la consulta</response>
        /// <response code="400">Si no se indicó la paginación</response>  
        /// <response code="404">Si no se encontró resultados</response> 
        [HttpGet("")]
        [ProducesResponseType(typeof(PaginatedItemsResponseViewModel<IngresanteResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        public async Task<IActionResult> ListarIngresantes([FromQuery] PaginatedItemsRequestViewModel<IngresanteRequestDto> peticion)
        {
            //valida
            if (peticion.PageSize == 0)
                return BadRequest();

            if (peticion.Filter == null) peticion.Filter = new IngresanteRequestDto();

            try
            {
                var result = await _ingresanteQueries.ListarIngresantes(peticion);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}