using AcademicoOds.Api.Application.ViewModels;
using AcademicoOds.Api.Application.ViewModels.IngresanteModel;
using AcademicoOds.Api.Infrastructure.Helpers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.Queries
{
    public class IngresanteQueries : IIngresanteQueries
    {
        private string _connectionString = string.Empty;

        public IngresanteQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<IngresanteResponseDto>> ListarIngresantes(PaginatedItemsRequestViewModel<IngresanteRequestDto> request)
        {
            var rpta = new List<IngresanteResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@pageIndex", request.Skip, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@codigoEntidad", request.Filter.CodigoEntidad, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoTipoIngresante", request.Filter.CodigoTipoIngresante, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoProcesoAdmision", request.Filter.CodigoProcesoAdmision, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoNivelAcademico", request.Filter.CodigoNivelAcademico, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoFilial", request.Filter.CodigoFilial, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoFacultad", request.Filter.CodigoFacultad, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoPrograma", request.Filter.CodigoPrograma, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoModalidadIngreso", request.Filter.CodigoModalidadIngreso, DbType.String, ParameterDirection.Input);
                parameter.Add("@codigoTipoDocumento", request.Filter.CodigoTipoDocumento, DbType.String, ParameterDirection.Input);
                parameter.Add("@numeroDocumento", request.Filter.NumeroDocumento, DbType.String, ParameterDirection.Input);

                var filtros = "";
                if (!string.IsNullOrEmpty(request.Filter.CodigoEntidad))
                    filtros += " and CODIGO_ENTIDAD in (" + DapperHelper.ObtenValoresCadena(request.Filter.CodigoEntidad) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoTipoIngresante))
                    filtros += " and ID_TIPO_INGRESANTE in (" + DapperHelper.ObtenValoresInt(request.Filter.CodigoTipoIngresante) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoProcesoAdmision))
                    filtros += " and PROCESO_ADMISION in (" + DapperHelper.ObtenValoresCadena(request.Filter.CodigoProcesoAdmision) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoNivelAcademico))
                    filtros += " and ID_NIVEL_ACADEMICO in (" + DapperHelper.ObtenValoresInt(request.Filter.CodigoNivelAcademico) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoFilial))
                    filtros += " and CODIGO_FILIAL in (" + DapperHelper.ObtenValoresCadena(request.Filter.CodigoFilial) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoFacultad))
                    filtros += " and CODIGO_UNIDAD in (" + DapperHelper.ObtenValoresInt(request.Filter.CodigoFacultad) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoPrograma))
                    filtros += " and CODIGO_PROGRAMA in (" + DapperHelper.ObtenValoresInt(request.Filter.CodigoPrograma) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoModalidadIngreso))
                    filtros += " and ID_MODALIDAD_INGRESO in (" + DapperHelper.ObtenValoresInt(request.Filter.CodigoModalidadIngreso) + ")";
                if (!string.IsNullOrEmpty(request.Filter.CodigoTipoDocumento))
                    filtros += " and ID_TIPO_DOCUMENTO = @codigoTipoDocumento";
                if (!string.IsNullOrEmpty(request.Filter.NumeroDocumento))
                    filtros += " and NUMERO_DOCUMENTO = @numeroDocumento";

                //if (!string.IsNullOrEmpty(request.Filter.CodigoEntidad))
                //    filtros += " and CODIGO_ENTIDAD in (@codigoEntidad)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoTipoIngresante))
                //    filtros += " and ID_TIPO_INGRESANTE in (@codigoTipoIngresante)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoProcesoAdmision))
                //    filtros += " and PROCESO_ADMISION in (@codigoProcesoAdmision)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoNivelAcademico))
                //    filtros += " and ID_NIVEL_ACADEMICO in (@codigoNivelAcademico)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoFilial))
                //    filtros += " and CODIGO_FILIAL in (@codigoFilial)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoFacultad))
                //    filtros += " and CODIGO_UNIDAD in (@codigoFacultad)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoPrograma))
                //    filtros += " and CODIGO_PROGRAMA in (@codigoPrograma)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoModalidadIngreso))
                //    filtros += " and ID_MODALIDAD_INGRESO in (@codigoModalidadIngreso)";
                //if (!string.IsNullOrEmpty(request.Filter.CodigoTipoDocumento))
                //    filtros += " and ID_TIPO_DOCUMENTO = @codigoTipoDocumento";
                //if (!string.IsNullOrEmpty(request.Filter.NumeroDocumento))
                //    filtros += " and NUMERO_DOCUMENTO = @numeroDocumento";

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
                   @"select count(ID_INGRESOS) 'total'
                        from [dbo].[ods_ingresos]
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
                          ,[CODIGO_FILIAL]
                          ,[ES_SEDE_PRINCIPAL]
                          ,[CODIGO_UBIGEO_INEI_FILIAL]
                          ,[DEPARTAMENTO_FILIAL]
                          ,[PROVINCIA_FILIAL]
                          ,[ESTADO_VIGENCIA_FILIAL]
                          ,[FECHA_VIGENCIA_INICIO_FILIAL]
                          ,[FECHA_VIGENCIA_FIN_FILIAL]
                          ,[CODIGO_UNIDAD]
                          ,[TIPO_UNIDAD]
                          ,[NAMBRE_UNIDAD]
                          ,[ESTADO_VIGENCIA_UNIDAD]
                          ,[FECHA_VIGENCIA_INICIO_UNIDAD]
                          ,[FECHA_VIGENCIA_FIN_UNIDAD]
                          ,[CODIGO_PROGRAMA]
                          ,[CODIGO_CLASIFICADOR_CINE]
                          ,[CLASIFICADOR_CINE]
                          ,[NOMBRE_PROGRAMA]
                          ,[TIPO_AUTORIZACION_PROGRAMA]
                          ,[TIPO_NIVEL_ACADEMICO]
                          ,[NIVEL_ACADEMICO]
                          ,[ESTADO_VIGENCIA_PROGRAMA]
                          ,[FECHA_VIGENCIA_INICIO_PROGRAMA]
                          ,[FECHA_VIGENCIA_FIN_PROGRAMA]
                          ,[TIPO_PROCESO_ADMISION]
                          ,[PROCESO_ADMISION]
                          ,[ANIO]
                          ,[FECHA_INGRESO]
                          ,[TIPO_INGRESANTE]
                          ,[MODALIDAD_INGRESO]
                          ,[MODALIDAD_ESTUDIO]
	                      ,GUID_PERSONA
	                      ,GUID_INGRESANTE
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
                        from [dbo].[ods_ingresos]
                        where 1=1 " + filtros + @"
                        ORDER BY ID_INGRESOS
                        OFFSET @pageIndex ROWS FETCH NEXT @pageSize ROWS ONLY", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<IngresanteResponseDto>(request.Skip, request.PageSize, count, rpta);
            }


        }

        private List<IngresanteResponseDto> MapItems(dynamic result)
        {
            var lista = new List<IngresanteResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new IngresanteResponseDto
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
                    CodigoFilial = item.CODIGO_FILIAL,
                    EsSedePrincipal = item.ES_SEDE_PRINCIPAL,
                    CodigoUbigeoIneiFilial = item.CODIGO_UBIGEO_INEI_FILIAL,
                    DepartamentoFilial = item.DEPARTAMENTO_FILIAL,
                    ProvinciaFilial = item.PROVINCIA_FILIAL,
                    EstadoVigenciaFilial = item.ESTADO_VIGENCIA_FILIAL,
                    FechaVigenciaInicioFilial = item.FECHA_VIGENCIA_INICIO_FILIAL,
                    FechaVigenciaFinFilial = item.FECHA_VIGENCIA_FIN_FILIAL,
                    CodigoFacultadUnidad = item.CODIGO_UNIDAD,
                    TipoUnidad = item.TIPO_UNIDAD,
                    NambreUnidad = item.NAMBRE_UNIDAD,
                    EstadoVigenciaUnidad = item.ESTADO_VIGENCIA_UNIDAD,
                    FechaVigenciaInicioUnidad = item.FECHA_VIGENCIA_INICIO_UNIDAD,
                    FechaVigenciaFinUnidad = item.FECHA_VIGENCIA_FIN_UNIDAD,
                    CodigoPrograma = item.CODIGO_PROGRAMA,
                    CodigoClasificadorCime = item.CODIGO_CLASIFICADOR_CINE,
                    ClasificadorCime = item.CLASIFICADOR_CINE,
                    NombrePrograma = item.NOMBRE_PROGRAMA,
                    TipoAutorizacionPrograma = item.TIPO_AUTORIZACION_PROGRAMA,
                    TipoNivelAcademico = item.TIPO_NIVEL_ACADEMICO,
                    NivelAcademico = item.NIVEL_ACADEMICO,
                    EstadoVigenciaPrograma = item.ESTADO_VIGENCIA_PROGRAMA,
                    FechaVigenciaInicioPrograma = item.FECHA_VIGENCIA_INICIO_PROGRAMA,
                    FechaVigenciaFinPrograma = item.FECHA_VIGENCIA_FIN_PROGRAMA,
                    TipoProcesoAdmision = item.TIPO_PROCESO_ADMISION,
                    ProcesoAdmision = item.PROCESO_ADMISION,
                    Anio = item.ANIO,
                    FechaIngreso = item.FECHA_INGRESO,
                    TipoIngresante = item.TIPO_INGRESANTE,
                    ModalidadIngreso = item.MODALIDAD_INGRESO,
                    ModalidadEstudio = item.MODALIDAD_ESTUDIO,
                    GuidPersona = item.GUID_PERSONA,
                    GuidIngresante = item.GUID_INGRESANTE,
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
