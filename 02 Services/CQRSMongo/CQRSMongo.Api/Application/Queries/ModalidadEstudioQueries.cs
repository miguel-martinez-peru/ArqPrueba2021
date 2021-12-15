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
    public class ModalidadEstudioQueries : IModalidadEstudioQueries
    {
        private string _connectionString = string.Empty;

        public ModalidadEstudioQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<ModalidadEstudioResponseDto>> Listar(ModalidadEstudioRequestDto request)
        {
            var rpta = new List<ModalidadEstudioResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();

                var count = connection.QueryFirst<int>(
                   @"select count(ID_MODALIDAD_ESTUDIO) 'total'
                        from [dbo].[ods_modalidad_estudio]
                        where 1=1", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [ID_MODALIDAD_ESTUDIO]
                          ,[DESCRIPCION]
                        from [dbo].[ods_modalidad_estudio]
                        where 1=1", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<ModalidadEstudioResponseDto>(0, 0, count, rpta);
            }


        }

        private List<ModalidadEstudioResponseDto> MapItems(dynamic result)
        {
            var lista = new List<ModalidadEstudioResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new ModalidadEstudioResponseDto
                {
                    IdModalidadEstudio = item.ID_MODALIDAD_ESTUDIO,
                    Descripcion = item.DESCRIPCION
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
