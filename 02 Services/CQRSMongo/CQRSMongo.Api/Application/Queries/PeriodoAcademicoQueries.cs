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
    public class PeriodoAcademicoQueries : IPeriodoAcademicoQueries
    {
        private string _connectionString = string.Empty;

        public PeriodoAcademicoQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<PeriodoAcademicoResponseDto>> Listar(PeriodoAcademicoRequestDto request)
        {
            var rpta = new List<PeriodoAcademicoResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@entidad", request.CodigoEntidad, DbType.String, ParameterDirection.Input);

                var count = connection.QueryFirst<int>(
                   @"select count([ID_PERIODO_ACADEMICO]) 'total'
                        from [dbo].[ods_periodo_academico]
                        where CODIGO_ENTIDAD=@entidad", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [ANIO_NUMERO]
                          ,[FECHA_INICIO_PERIODO]
                          ,[FECHA_FIN_PERIODO]
                        from [dbo].[ods_periodo_academico]
                        where CODIGO_ENTIDAD=@entidad", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<PeriodoAcademicoResponseDto>(0, 0, count, rpta);
            }


        }

        private List<PeriodoAcademicoResponseDto> MapItems(dynamic result)
        {
            var lista = new List<PeriodoAcademicoResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new PeriodoAcademicoResponseDto
                {
                    AnioNumero = item.ANIO_NUMERO,
                    FechaInicioPeriodo = item.FECHA_INICIO_PERIODO,
                    FechaFinPeriodo = item.FECHA_FIN_PERIODO
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
