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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Sunedu.Core;

namespace AcademicoOds.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/v1/tipo-ingresante")]
    [ApiController]
    public class TipoIngresanteController : BaseController
    {
        private readonly ITipoIngresanteQueries _TipoIngresanteQueries;

        public TipoIngresanteController(ITipoIngresanteQueries TipoIngresanteQueries
            , IConfiguration configuration) : base(configuration)
        {
            _TipoIngresanteQueries = TipoIngresanteQueries ?? throw new ArgumentNullException(nameof(TipoIngresanteQueries));
        }


        /// <summary>
        /// Permite consultar los datos de los tipos de ingresantes desde el Academico ODS
        /// </summary>
        /// <response code="200">Devuelve la lista de resultados de la consulta</response>
        /// <response code="400">Si no se indicó la paginación</response>  
        /// <response code="404">Si no se encontró resultados</response> 
        [HttpGet("")]
        [ProducesResponseType(typeof(PaginatedItemsResponseViewModel<TipoIngresanteResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status404NotFound)]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        public async Task<IActionResult> Listar([FromQuery] TipoIngresanteRequestDto peticion)
        {
            try
            {
                var result = await _TipoIngresanteQueries.Listar(peticion);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

    }
}