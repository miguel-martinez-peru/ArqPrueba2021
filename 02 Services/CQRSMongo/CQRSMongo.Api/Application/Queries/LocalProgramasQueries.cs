using AcademicoOds.Api.Application.ViewModels;
using AcademicoOds.Api.Application.ViewModels.DocenteModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.Queries
{
    public class LocalProgramasQueries : ILocalProgramasQueries
    {
        private string _connectionString = string.Empty;

        public LocalProgramasQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<EntidadFilialResponseDto>> ListarFilial(LocalProgramasRequestDto request)
        {
            var rpta = new List<EntidadFilialResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@entidad", request.CodigoEntidad, DbType.String, ParameterDirection.Input);

                var count = connection.QueryFirst<int>(
                   @"select count(CODIGO_FILIAL) from (
                        select distinct CODIGO_FILIAL, PROVINCIA_FILIAL
                        FROM [dbo].[ods_local_programas]
                        where CODIGO_ENTIDAD=@entidad) a", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select distinct [CODIGO_FILIAL]
                          ,[PROVINCIA_FILIAL]
                        from [dbo].[ods_local_programas]
                        where CODIGO_ENTIDAD=@entidad", parameter
                    );

                    rpta = MapItemsFilial(result);
                }

                return new PaginatedItemsResponseViewModel<EntidadFilialResponseDto>(0, 0, count, rpta);
            }
        }

        public async Task<PaginatedItemsResponseViewModel<EntidadLocalResponseDto>> ListarLocal(LocalProgramasRequestDto request)
        {
            var rpta = new List<EntidadLocalResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@entidad", request.CodigoEntidad, DbType.String, ParameterDirection.Input);

                var count = connection.QueryFirst<int>(
                   @"select count(CODIGO_LOCAL) from (
                        select distinct CODIGO_LOCAL, DISTRITO_LOCAL, DIRECION_LOCAL, CODIGO_FILIAL
                        FROM [dbo].[ods_local_programas]
                        where CODIGO_ENTIDAD=@entidad) a", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select distinct CODIGO_LOCAL, DISTRITO_LOCAL, DIRECION_LOCAL, CODIGO_FILIAL
                        from [dbo].[ods_local_programas]
                        where CODIGO_ENTIDAD=@entidad", parameter
                    );

                    rpta = MapItemsLocal(result);
                }

                return new PaginatedItemsResponseViewModel<EntidadLocalResponseDto>(0, 0, count, rpta);
            }
        }

        public async Task<PaginatedItemsResponseViewModel<EntidadFacultadResponseDto>> ListarFacultad(LocalProgramasRequestDto request)
        {
            var rpta = new List<EntidadFacultadResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@entidad", request.CodigoEntidad, DbType.String, ParameterDirection.Input);

                var count = connection.QueryFirst<int>(
                   @"select count(CODIGO_UNIDAD) from (
                        select distinct CODIGO_UNIDAD, NOMBRE_UNIDAD
                        FROM [dbo].[ods_local_programas]
                        where CODIGO_ENTIDAD=@entidad) a", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select distinct CODIGO_UNIDAD, NOMBRE_UNIDAD
                        from [dbo].[ods_local_programas]
                        where CODIGO_ENTIDAD=@entidad", parameter
                    );

                    rpta = MapItemsFacultad(result);
                }

                return new PaginatedItemsResponseViewModel<EntidadFacultadResponseDto>(0, 0, count, rpta);
            }
        }

        public async Task<PaginatedItemsResponseViewModel<EntidadProgramaResponseDto>> ListarPrograma(LocalProgramasRequestDto request)
        {
            var rpta = new List<EntidadProgramaResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@entidad", request.CodigoEntidad, DbType.String, ParameterDirection.Input);

                var count = connection.QueryFirst<int>(
                   @"select count(CODIGO_PROGRAMA) from (
                        select distinct CODIGO_PROGRAMA, NOMBRE_PROGRAMA, CODIGO_UNIDAD
                        FROM [dbo].[ods_local_programas]
                        where CODIGO_ENTIDAD=@entidad) a", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select distinct CODIGO_PROGRAMA, NOMBRE_PROGRAMA, CODIGO_UNIDAD
                        from [dbo].[ods_local_programas]
                        where CODIGO_ENTIDAD=@entidad", parameter
                    );

                    rpta = MapItemsPrograma(result);
                }

                return new PaginatedItemsResponseViewModel<EntidadProgramaResponseDto>(0, 0, count, rpta);
            }
        }

        private List<EntidadFilialResponseDto> MapItemsFilial(dynamic result)
        {
            var lista = new List<EntidadFilialResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new EntidadFilialResponseDto
                {
                    CodigoFilial = item.CODIGO_FILIAL,
                    Descripcion = item.PROVINCIA_FILIAL
                };
                lista.Add(temp);
            }

            return lista;
        }

        private List<EntidadLocalResponseDto> MapItemsLocal(dynamic result)
        {
            var lista = new List<EntidadLocalResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new EntidadLocalResponseDto
                {
                    CodigoLocal = item.CODIGO_LOCAL,
                    Descripcion = item.DISTRITO_LOCAL,
                    Direccion = item.DIRECION_LOCAL,
                    CodigoFilial = item.CODIGO_FILIAL
                };
                lista.Add(temp);
            }

            return lista;
        }

        private List<EntidadFacultadResponseDto> MapItemsFacultad(dynamic result)
        {
            var lista = new List<EntidadFacultadResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new EntidadFacultadResponseDto
                {
                    CodigoFacultad = item.CODIGO_UNIDAD,
                    Descripcion = item.NOMBRE_UNIDAD
                };
                lista.Add(temp);
            }

            return lista;
        }

        private List<EntidadProgramaResponseDto> MapItemsPrograma(dynamic result)
        {
            var lista = new List<EntidadProgramaResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new EntidadProgramaResponseDto
                {
                    CodigoPrograma = item.CODIGO_PROGRAMA,
                    Descripcion = item.NOMBRE_PROGRAMA,
                    CodigoFacultad = item.CODIGO_UNIDAD
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
