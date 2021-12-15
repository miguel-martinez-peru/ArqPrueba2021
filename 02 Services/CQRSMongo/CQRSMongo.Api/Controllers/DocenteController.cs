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
using AcademicoOds.Api.Application.ViewModels.DocenteModel;
using Sunedu.Transversal.Security.Auth.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AcademicoOds.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/v1/docente")]
    [ApiController]
    public class DocenteController : BaseController
    {
        private readonly IDocenteQueries _docenteQueries;
        private readonly ILogger<DocenteController> logger;

        public DocenteController(IDocenteQueries docenteQueries
            , IConfiguration configuration
            , ILogger<DocenteController> logger) : base(configuration)
        {
            _docenteQueries = docenteQueries ?? throw new ArgumentNullException(nameof(docenteQueries));
            this.logger = logger;
        }


        /// <summary>
        /// Permite consultar los datos de los docentes desde el Academico ODS
        /// </summary>
        /// <response code="200">Devuelve la lista de resultados de la consulta</response>
        /// <response code="400">Si no se indicó la paginación</response>  
        /// <response code="404">Si no se encontró resultados</response> 
        [HttpGet("")]
        [ProducesResponseType(typeof(PaginatedItemsResponseViewModel<DocenteResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        public async Task<IActionResult> ListarDocentes([FromQuery] PaginatedItemsRequestViewModel<DocenteRequestDto> peticion)
        {
            //valida
            if (peticion.PageSize == 0)
                return BadRequest();

            if (peticion.Filter == null) peticion.Filter = new DocenteRequestDto();

            try
            {
                var result = await _docenteQueries.ListarDocentes(peticion);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

    }
}