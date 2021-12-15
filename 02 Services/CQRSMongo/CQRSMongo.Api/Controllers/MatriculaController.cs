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
using AcademicoOds.Api.Application.ViewModels.MatriculaModel;
using Sunedu.Transversal.Security.Auth.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AcademicoOds.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/v1/matricula")]
    [ApiController]
    public class MatriculaController : BaseController
    {
        private readonly IMatriculaQueries _matriculaQueries;
        private readonly ILogger<MatriculaController> logger;

        public MatriculaController(IMatriculaQueries matriculaQueries
            , IConfiguration configuration
            , ILogger<MatriculaController> logger) : base(configuration)
        {
            _matriculaQueries = matriculaQueries ?? throw new ArgumentNullException(nameof(matriculaQueries));
            this.logger = logger;
        }


        /// <summary>
        /// Permite consultar los datos de los matriculados desde el Academico ODS
        /// </summary>
        /// <response code="200">Devuelve la lista de resultados de la consulta</response>
        /// <response code="400">Si no se indicó la paginación</response>  
        /// <response code="404">Si no se encontró resultados</response> 
        [HttpGet("")]
        [ProducesResponseType(typeof(PaginatedItemsResponseViewModel<MatriculaResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        public async Task<IActionResult> ListarMatriculas([FromQuery] PaginatedItemsRequestViewModel<MatriculaRequestDto> peticion)
        {
            //valida
            if (peticion.PageSize == 0)
                return BadRequest();

            if (peticion.Filter == null) peticion.Filter = new MatriculaRequestDto();

            try
            {
                var result = await _matriculaQueries.ListarMatriculas(peticion);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}