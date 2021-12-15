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
    [Route("api/v1/local-programas")]
    [ApiController]
    public class LocalProgramasController : BaseController
    {
        private readonly ILocalProgramasQueries _LocalProgramasQueries;

        public LocalProgramasController(ILocalProgramasQueries LocalProgramasQueries
            , IConfiguration configuration) : base(configuration)
        {
            _LocalProgramasQueries = LocalProgramasQueries ?? throw new ArgumentNullException(nameof(LocalProgramasQueries));
        }


        /// <summary>
        /// Permite consultar los datos de las filiales desde el Academico ODS
        /// </summary>
        /// <response code="200">Devuelve la lista de resultados de la consulta</response>
        /// <response code="400">Si no se indicó la paginación</response>  
        /// <response code="404">Si no se encontró resultados</response> 
        [HttpGet("filial")]
        [ProducesResponseType(typeof(PaginatedItemsResponseViewModel<EntidadFilialResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status404NotFound)]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        public async Task<IActionResult> ListarFilial([FromQuery] LocalProgramasRequestDto peticion)
        {
            try
            {
                var result = await _LocalProgramasQueries.ListarFilial(peticion);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Permite consultar los datos de los locales desde el Academico ODS
        /// </summary>
        /// <response code="200">Devuelve la lista de resultados de la consulta</response>
        /// <response code="400">Si no se indicó la paginación</response>  
        /// <response code="404">Si no se encontró resultados</response> 
        [HttpGet("local")]
        [ProducesResponseType(typeof(PaginatedItemsResponseViewModel<EntidadLocalResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status404NotFound)]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        public async Task<IActionResult> ListarLocal([FromQuery] LocalProgramasRequestDto peticion)
        {
            try
            {
                var result = await _LocalProgramasQueries.ListarLocal(peticion);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Permite consultar los datos de las facultades desde el Academico ODS
        /// </summary>
        /// <response code="200">Devuelve la lista de resultados de la consulta</response>
        /// <response code="400">Si no se indicó la paginación</response>  
        /// <response code="404">Si no se encontró resultados</response> 
        [HttpGet("facultad")]
        [ProducesResponseType(typeof(PaginatedItemsResponseViewModel<EntidadFacultadResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status404NotFound)]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        public async Task<IActionResult> ListarFacultad([FromQuery] LocalProgramasRequestDto peticion)
        {
            try
            {
                var result = await _LocalProgramasQueries.ListarFacultad(peticion);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Permite consultar los datos de los programas desde el Academico ODS
        /// </summary>
        /// <response code="200">Devuelve la lista de resultados de la consulta</response>
        /// <response code="400">Si no se indicó la paginación</response>  
        /// <response code="404">Si no se encontró resultados</response> 
        [HttpGet("programa")]
        [ProducesResponseType(typeof(PaginatedItemsResponseViewModel<EntidadProgramaResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResult), StatusCodes.Status404NotFound)]
        //[ServiceFilter(typeof(AuthorizeCheckActionFilter))]
        public async Task<IActionResult> ListarPrograma([FromQuery] LocalProgramasRequestDto peticion)
        {
            try
            {
                var result = await _LocalProgramasQueries.ListarPrograma(peticion);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}