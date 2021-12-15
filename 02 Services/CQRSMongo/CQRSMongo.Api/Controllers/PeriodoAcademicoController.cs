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
    [Route("api/v1/periodo-academico")]
    [ApiController]
    public class PeriodoAcademicoController : BaseController
    {
        private readonly IPeriodoAcademicoQueries _PeriodoAcademicoQueries;

        public PeriodoAcademicoController(IPeriodoAcademicoQueries PeriodoAcademicoQueries
            , IConfiguration configuration) : base(configuration)
        {
            _PeriodoAcademicoQueries = PeriodoAcademicoQueries ?? throw new ArgumentNullException(nameof(PeriodoAcademicoQueries));
        }


        /// <summary>
        /// Permite consultar los datos de los periodos academicos desde el Academico ODS
        /// </summary>
        /// <response code="200">Devuelve la lista de resultados de la consulta</response>
        /// <response code="400">Si no se indicó la paginación</response>  
        /// <response code="404">Si no se encontró resultados</response> 
        [HttpGet("")]
        [ProducesResponseType(typeof(PaginatedItemsResponseViewModel<PeriodoAcademicoResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status404NotFound)]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        public async Task<IActionResult> Listar([FromQuery] PeriodoAcademicoRequestDto peticion)
        {
            try
            {
                var result = await _PeriodoAcademicoQueries.Listar(peticion);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

    }
}