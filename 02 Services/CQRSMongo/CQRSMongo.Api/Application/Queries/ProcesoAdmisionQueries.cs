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
    public class ProcesoAdmisionQueries : IProcesoAdmisionQueries
    {
        private string _connectionString = string.Empty;

        public ProcesoAdmisionQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<ProcesoAdmisionResponseDto>> Listar(ProcesoAdmisionRequestDto request)
        {
            var rpta = new List<ProcesoAdmisionResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@entidad", request.CodigoEntidad, DbType.String, ParameterDirection.Input);

                var count = connection.QueryFirst<int>(
                   @"select count([ID_PROCESO_ADMISION]) 'total'
                        from [dbo].[ods_proceso_admision]
                        where CODIGO_ENTIDAD=@entidad", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [ANIO_NUMERO]
                          ,[FECHA_INICIO_PROCESO]
                          ,[FECHA_FIN_PROCESO]
                        from [dbo].[ods_proceso_admision]
                        where CODIGO_ENTIDAD=@entidad", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<ProcesoAdmisionResponseDto>(0, 0, count, rpta);
            }


        }

        private List<ProcesoAdmisionResponseDto> MapItems(dynamic result)
        {
            var lista = new List<ProcesoAdmisionResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new ProcesoAdmisionResponseDto
                {
                    AnioNumero = item.ANIO_NUMERO,
                    FechaInicioProceso = item.FECHA_INICIO_PROCESO,
                    FechaFinProceso = item.FECHA_FIN_PROCESO
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
