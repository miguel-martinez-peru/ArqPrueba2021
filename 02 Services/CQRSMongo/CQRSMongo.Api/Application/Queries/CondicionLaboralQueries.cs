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
    public class CondicionLaboralQueries : ICondicionLaboralQueries
    {
        private string _connectionString = string.Empty;

        public CondicionLaboralQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<CondicionLaboralResponseDto>> Listar(CondicionLaboralRequestDto request)
        {
            var rpta = new List<CondicionLaboralResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();

                var count = connection.QueryFirst<int>(
                   @"select count(ID_CONDICION_LABORAL) 'total'
                        from [dbo].[ods_condicion_laboral]
                        where 1=1", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [ID_CONDICION_LABORAL]
                          ,[DESCRIPCION]
                        from [dbo].[ods_condicion_laboral]
                        where 1=1", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<CondicionLaboralResponseDto>(0, 0, count, rpta);
            }


        }

        private List<CondicionLaboralResponseDto> MapItems(dynamic result)
        {
            var lista = new List<CondicionLaboralResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new CondicionLaboralResponseDto
                {
                    IdCondicionLaboral = item.ID_CONDICION_LABORAL,
                    Descripcion = item.DESCRIPCION
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
