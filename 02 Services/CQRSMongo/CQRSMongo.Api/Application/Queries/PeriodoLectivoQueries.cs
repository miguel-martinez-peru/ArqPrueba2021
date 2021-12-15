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
    public class PeriodoLectivoQueries : IPeriodoLectivoQueries
    {
        private string _connectionString = string.Empty;

        public PeriodoLectivoQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<PeriodoLectivoResponseDto>> Listar(PeriodoLectivoRequestDto request)
        {
            var rpta = new List<PeriodoLectivoResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();

                var count = connection.QueryFirst<int>(
                   @"select count(ID_PERIODO_LECTIVO) 'total'
                        from [dbo].[ods_periodo_lectivo]
                        where 1=1", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [ID_PERIODO_LECTIVO]
                          ,[DESCRIPCION]
                        from [dbo].[ods_periodo_lectivo]
                        where 1=1", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<PeriodoLectivoResponseDto>(0, 0, count, rpta);
            }


        }

        private List<PeriodoLectivoResponseDto> MapItems(dynamic result)
        {
            var lista = new List<PeriodoLectivoResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new PeriodoLectivoResponseDto
                {
                    IdPeriodoLectivo = item.ID_PERIODO_LECTIVO,
                    Descripcion = item.DESCRIPCION
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
