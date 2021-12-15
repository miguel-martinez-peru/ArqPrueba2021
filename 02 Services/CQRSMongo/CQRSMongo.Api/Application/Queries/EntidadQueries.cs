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
    public class EntidadQueries : IEntidadQueries
    {
        private string _connectionString = string.Empty;

        public EntidadQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<EntidadResponseDto>> Listar(EntidadRequestDto request)
        {
            var rpta = new List<EntidadResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();

                var count = connection.QueryFirst<int>(
                   @"select count(ID_ENTIDAD) 'total'
                        from [dbo].[ods_entidad]
                        where 1=1", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [ID_ENTIDAD]
                          ,[CODIGO_ENTIDAD]
                          ,[CODIGO_ENTIDAD_INEI]
                          ,[NOMBRE]
                          ,[SIGLAS]
                        from [dbo].[ods_entidad]
                        where 1=1", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<EntidadResponseDto>(0, 0, count, rpta);
            }


        }

        private List<EntidadResponseDto> MapItems(dynamic result)
        {
            var lista = new List<EntidadResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new EntidadResponseDto
                {
                    IdEntidad = item.ID_ENTIDAD,
                    CodigoEntidad = item.CODIGO_ENTIDAD,
                    CodigoEntidadInei = item.CODIGO_ENTIDAD_INEI,
                    Nombre = item.NOMBRE,
                    Siglas = item.SIGLAS
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
