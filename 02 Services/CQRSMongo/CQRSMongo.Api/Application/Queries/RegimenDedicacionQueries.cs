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
    public class RegimenDedicacionQueries : IRegimenDedicacionQueries
    {
        private string _connectionString = string.Empty;

        public RegimenDedicacionQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<RegimenDedicacionResponseDto>> Listar(RegimenDedicacionRequestDto request)
        {
            var rpta = new List<RegimenDedicacionResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();

                var count = connection.QueryFirst<int>(
                   @"select count(ID_REGIMEN_DEDICACION) 'total'
                        from [dbo].[ods_regimen_dedicacion]
                        where 1=1", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [ID_REGIMEN_DEDICACION]
                          ,[DESCRIPCION]
                        from [dbo].[ods_regimen_dedicacion]
                        where 1=1", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<RegimenDedicacionResponseDto>(0, 0, count, rpta);
            }


        }

        private List<RegimenDedicacionResponseDto> MapItems(dynamic result)
        {
            var lista = new List<RegimenDedicacionResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new RegimenDedicacionResponseDto
                {
                    IdRegimenDedicacion = item.ID_REGIMEN_DEDICACION,
                    Descripcion = item.DESCRIPCION
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
