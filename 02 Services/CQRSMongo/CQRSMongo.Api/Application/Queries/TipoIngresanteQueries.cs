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
    public class TipoIngresanteQueries : ITipoIngresanteQueries
    {
        private string _connectionString = string.Empty;

        public TipoIngresanteQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<TipoIngresanteResponseDto>> Listar(TipoIngresanteRequestDto request)
        {
            var rpta = new List<TipoIngresanteResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();

                var count = connection.QueryFirst<int>(
                   @"select count(ID_TIPO_INGRESANTE) 'total'
                        from [dbo].[ods_tipo_ingresante]
                        where 1=1", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                       @"select [ID_TIPO_INGRESANTE]
                          ,[DESCRIPCION]
                        from [dbo].[ods_tipo_ingresante]
                        where 1=1", parameter
                        );

                    rpta = MapItems(result);
                }
                return new PaginatedItemsResponseViewModel<TipoIngresanteResponseDto>(0, 0, count, rpta);
            }


        }

        private List<TipoIngresanteResponseDto> MapItems(dynamic result)
        {
            var lista = new List<TipoIngresanteResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new TipoIngresanteResponseDto
                {
                    IdTipoIngresante = item.ID_TIPO_INGRESANTE,
                    Descripcion = item.DESCRIPCION
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
