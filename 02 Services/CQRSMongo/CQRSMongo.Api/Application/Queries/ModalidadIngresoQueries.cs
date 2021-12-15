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
    public class ModalidadIngresoQueries : IModalidadIngresoQueries
    {
        private string _connectionString = string.Empty;

        public ModalidadIngresoQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<ModalidadIngresoResponseDto>> Listar(ModalidadIngresoRequestDto request)
        {
            var rpta = new List<ModalidadIngresoResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();

                var count = connection.QueryFirst<int>(
                   @"select count(ID_MODALIDAD_INGRESO) 'total'
                        from [dbo].[ods_modalidad_ingreso]
                        where 1=1", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [ID_MODALIDAD_INGRESO]
                          ,[DESCRIPCION]
                        from [dbo].[ods_modalidad_ingreso]
                        where 1=1", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<ModalidadIngresoResponseDto>(0, 0, count, rpta);
            }


        }

        private List<ModalidadIngresoResponseDto> MapItems(dynamic result)
        {
            var lista = new List<ModalidadIngresoResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new ModalidadIngresoResponseDto
                {
                    IdModalidadIngreso = item.ID_MODALIDAD_INGRESO,
                    Descripcion = item.DESCRIPCION
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
