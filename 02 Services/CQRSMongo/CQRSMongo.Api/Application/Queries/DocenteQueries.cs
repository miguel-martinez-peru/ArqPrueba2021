using AcademicoOds.Api.Application.ViewModels;
using AcademicoOds.Api.Application.ViewModels.DocenteModel;
using AcademicoOds.Api.Infrastructure.Helpers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.Queries
{
    public class DocenteQueries : IDocenteQueries
    {
        private string _connectionString = string.Empty;

        public DocenteQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<DocenteResponseDto>> ListarDocentes(PaginatedItemsRequestViewModel<DocenteRequestDto> request)
        {
            var rpta = new List<DocenteResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@pageIndex", request.Skip, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@codigoEntidad", request.Filter.CodigoEntidad, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoTipoDocumento", request.Filter.CodigoTipoDocumento, DbType.String, ParameterDirection.Input);
                parameter.Add("@numeroDocumento", request.Filter.NumeroDocumento, DbType.String, ParameterDirection.Input);
                parameter.Add("@horasLectivasDesde", request.Filter.HorasLectivasDesde, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@horasLectivasHasta", request.Filter.HorasLectivasHasta, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@codigoCondicionLaboral", request.Filter.CodigoCondicionLaboral, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoRegimenDedicacion", request.Filter.CodigoRegimenDedicacion, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoEstadoDocente", request.Filter.CodigoEstadoDocente, DbType.String, ParameterDirection.Input);

                var filtros = "";
                if (!string.IsNullOrEmpty(request.Filter.CodigoEntidad))
                    filtros += " and CODIGO_ENTIDAD in (" + DapperHelper.ObtenValoresCadena(request.Filter.CodigoEntidad) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoTipoDocumento))
                    filtros += " and ID_TIPO_DOCUMENTO = @codigoTipoDocumento";
                if (!string.IsNullOrEmpty(request.Filter.NumeroDocumento))
                    filtros += " and NUMERO_DOCUMENTO = @numeroDocumento";
                if (request.Filter.HorasLectivasDesde != null)
                    filtros += " and HORAS_LECTIVAS_SEMANALES >= @horasLectivasDesde";
                if (request.Filter.HorasLectivasHasta != null)
                    filtros += " and HORAS_LECTIVAS_SEMANALES <= @horasLectivasHasta";
                if (!string.IsNullOrEmpty(request.Filter.CodigoCondicionLaboral))
                    filtros += " and ID_CONDICION_LABORAL in (" + DapperHelper.ObtenValoresInt(request.Filter.CodigoCondicionLaboral) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoRegimenDedicacion))
                    filtros += " and ID_REGIMEN_DEDICACION in (" + DapperHelper.ObtenValoresInt(request.Filter.CodigoRegimenDedicacion) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoEstadoDocente))
                    filtros += " and ID_ESTADO_DOCENTE in (" + DapperHelper.ObtenValoresInt(request.Filter.CodigoEstadoDocente) + ")";

                //if (!string.IsNullOrEmpty(request.Filter.CodigoEntidad))
                //    filtros += " and CODIGO_ENTIDAD in (@codigoEntidad)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoTipoDocumento))
                //    filtros += " and ID_TIPO_DOCUMENTO = @codigoTipoDocumento";
                //if (!string.IsNullOrEmpty(request.Filter.NumeroDocumento))
                //    filtros += " and NUMERO_DOCUMENTO = @numeroDocumento";
                //if (request.Filter.HorasLectivasDesde != null)
                //    filtros += " and HORAS_LECTIVAS_SEMANALES >= @horasLectivasDesde";
                //if (request.Filter.HorasLectivasHasta != null)
                //    filtros += " and HORAS_LECTIVAS_SEMANALES <= @horasLectivasHasta";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoCondicionLaboral))
                //    filtros += " and ID_CONDICION_LABORAL in (@codigoCondicionLaboral)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoRegimenDedicacion))
                //    filtros += " and ID_REGIMEN_DEDICACION in (@codigoRegimenDedicacion)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoEstadoDocente))
                //    filtros += " and ID_ESTADO_DOCENTE in (@codigoEstadoDocente)";

                if (request.Filter.FechaRegistroDesde != null)
                {
                    parameter.Add("@fechaRegistroDesde", request.Filter.FechaRegistroDesde, DbType.DateTime, ParameterDirection.Input);
                    filtros += " and (([FECHA_MODIFICACION] is null AND [FECHA_CREACION] >= @fechaRegistroDesde) OR ([FECHA_MODIFICACION] is not null AND [FECHA_MODIFICACION] >= @fechaRegistroDesde))";
                }
                if (request.Filter.FechaRegistroHasta != null)
                {
                    request.Filter.FechaRegistroHasta = request.Filter.FechaRegistroHasta.Value.AddDays(1).AddSeconds(-1);
                    parameter.Add("@fechaRegistroHasta", request.Filter.FechaRegistroHasta, DbType.DateTime, ParameterDirection.Input);
                    filtros += " and (([FECHA_MODIFICACION] is null AND [FECHA_CREACION] <= @fechaRegistroHasta) OR ([FECHA_MODIFICACION] is not null AND [FECHA_MODIFICACION] <= @fechaRegistroHasta))";
                }

                var count = connection.QueryFirst<int>(
                   @"select count(ID_DOCENTES) 'total'
                        from [dbo].[ods_docentes]
                        where 1=1" + filtros, parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [CODIGO_ENTIDAD]
                          ,[CODIGO_INEI_ENTIDAD]
                          ,[NOMBRE_ENTIDAD]
                          ,[TIPO_AUTORIZACION_ENTIDAD]
                          ,[TIPO_ENTIDAD]
                          ,[TIPO_GESTION]
                          ,[CODIGO_UBIGEO_INEI_ENTIDAD]
                          ,[DEPARTAMENTO_ENTIDAD]
                          ,[PROVINCIA_ENTIDAD]
                          ,[DISTRITO_ENTIDAD]
                          ,[ESTADO_VIGENCIA_ENTIDAD]
                          ,[FECHA_VIGENCIA_INICIO_ENTIDAD]
                          ,[FECHA_VIGENCIA_FIN_ENTIDAD]
                          ,[TIPO_NIVEL_ACADEMICO]
                          ,[NIVEL_ACADEMICO]
                          ,[PERIODO_ACADEMICO]
                          ,[PERIODO_LECTIVO]
                          ,[ANIO]
                          ,[GRADO_ACADEMICO]
                          ,[CONDICION_LABORAL]
                          ,[REGIMEN_DEDICACION]
                          ,[IDIOMA_DOCENTE]
                          ,[TIPO_IDIOMA_DOCENTE]
                          ,[ESTADO_DOCENTE]
                          ,[TIPO_ACTIVIDAD_DOCENTE]
                          ,[ACTIVIDAD_DOCENTE]
                          ,[HORAS_LECTIVAS_SEMANALES]
                          ,[HORAS_NO_LECTIVAS_SEMANALES]
	                      ,GUID_PERSONA
	                      ,GUID_DOCENTE
                          ,[TIPO_DOCUMENTO]
                          ,[NUMERO_DOCUMENTO]
                          ,[NOMBRES]
                          ,[PRIMER_APELLIDO]
                          ,[SEGUNDO_APELLIDO]
                          ,[TIENE_UN_APELLIDO]
                          ,[FECHA_NACIMIENTO]
                          ,[EDAD]
                          ,[SEXO]
                          ,[CODIGO_PAIS_NACIMIENTO]
                          ,[PAIS_NACIMIENTO]
                          ,[NACIONALIDAD]
                          ,[CODIGO_UBIGEO_INEI_NACIMIENTO]
                          ,[DEPARTAMENTO_NACIMIENTO]
                          ,[PROVINCIA_NACIMIENTO]
                          ,[DISTRITO_NACIMIENTO]
                          ,[CODIGO_UBIGEO_INEI_RESIDENCIA]
                          ,[DEPARTAMENTO_RESIDENCIA]
                          ,[PROVINCIA_RESIDENCIA]
                          ,[DISTRITO_RESIDENCIA]
                          ,[FECHA_CREACION]
                          ,[FECHA_MODIFICACION]
                        from [dbo].[ods_docentes]
                        where 1=1" + filtros + @"
                        ORDER BY ID_DOCENTES
                        OFFSET @pageIndex ROWS FETCH NEXT @pageSize ROWS ONLY", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<DocenteResponseDto>(request.Skip, request.PageSize, count, rpta);
            }


        }

        private List<DocenteResponseDto> MapItems(dynamic result)
        {
            var lista = new List<DocenteResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new DocenteResponseDto
                {
                    CodigoEntidad = item.CODIGO_ENTIDAD,
                    CodigoIneiEntidad = item.CODIGO_INEI_ENTIDAD,
                    NombreEntidad = item.NOMBRE_ENTIDAD,
                    TipoAutorizacionEntidad = item.TIPO_AUTORIZACION_ENTIDAD,
                    TipoEntidad = item.TIPO_ENTIDAD,
                    TipoGestion = item.TIPO_GESTION,
                    CodigoUbigeoIneiEntidad = item.CODIGO_UBIGEO_INEI_ENTIDAD,
                    DepartamentoEntidad = item.DEPARTAMENTO_ENTIDAD,
                    ProvinciaEntidad = item.PROVINCIA_ENTIDAD,
                    DistritoEntidad = item.DISTRITO_ENTIDAD,
                    EstadoVigenciaEntidad = item.ESTADO_VIGENCIA_ENTIDAD,
                    FechaVigenciaInicioEntidad = item.FECHA_VIGENCIA_INICIO_ENTIDAD,
                    FechaVigenciaFinEntidad = item.FECHA_VIGENCIA_FIN_ENTIDAD,
                    TipoNivelAcademico = item.TIPO_NIVEL_ACADEMICO,
                    NivelAcademico = item.NIVEL_ACADEMICO,
                    PeriodoAcademico = item.PERIODO_ACADEMICO,
                    PeriodoLectivo = item.PERIODO_LECTIVO,
                    Anio = item.ANIO,
                    GradoAcademico = item.GRADO_ACADEMICO,
                    CondicionLaboral = item.CONDICION_LABORAL,
                    RegimenDedicacion = item.REGIMEN_DEDICACION,
                    IdiomaDocente = item.IDIOMA_DOCENTE,
                    TipoIdiomaDocente = item.TIPO_IDIOMA_DOCENTE,
                    EstadoDocente = item.ESTADO_DOCENTE,
                    TipoActividadDocente = item.TIPO_ACTIVIDAD_DOCENTE,
                    ActividadDocente = item.ACTIVIDAD_DOCENTE,
                    HorasLectivasSemanales = item.HORAS_LECTIVAS_SEMANALES,
                    HorasNoLectivasSemanales = item.HORAS_NO_LECTIVAS_SEMANALES,
                    GuidPersona = item.GUID_PERSONA,
                    GuidDocente = item.GUID_DOCENTE,
                    TipoDocumento = item.TIPO_DOCUMENTO,
                    NumeroDocumento = item.NUMERO_DOCUMENTO,
                    Nombres = item.NOMBRES,
                    PrimerApellido = item.PRIMER_APELLIDO,
                    SegundoApellido = item.SEGUNDO_APELLIDO,
                    TieneUnApellido = item.TIENE_UN_APELLIDO,
                    FechaNacimiento = item.FECHA_NACIMIENTO,
                    Edad = item.EDAD,
                    Sexo = item.SEXO,
                    CodigoPaisNacimiento = item.CODIGO_PAIS_NACIMIENTO,
                    PaisNacimiento = item.PAIS_NACIMIENTO,
                    Nacionalidad = item.NACIONALIDAD,
                    CodigoUbigeoIneiNacimiento = item.CODIGO_UBIGEO_INEI_NACIMIENTO,
                    DepartamentoNacimiento = item.DEPARTAMENTO_NACIMIENTO,
                    ProvinciaNacimiento = item.PROVINCIA_NACIMIENTO,
                    DistritoNacimiento = item.DISTRITO_NACIMIENTO,
                    CodigoUbigeoIneiResidencia = item.CODIGO_UBIGEO_INEI_RESIDENCIA,
                    DepartamentoResidencia = item.DEPARTAMENTO_RESIDENCIA,
                    ProvinciaResidencia = item.PROVINCIA_RESIDENCIA,
                    DistritoResidencia = item.DISTRITO_RESIDENCIA,
                    FechaCreacion = item.FECHA_CREACION,
                    FechaActualizacion = item.FECHA_MODIFICACION
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
