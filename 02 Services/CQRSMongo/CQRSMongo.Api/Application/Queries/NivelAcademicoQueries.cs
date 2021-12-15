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
    public class NivelAcademicoQueries : INivelAcademicoQueries
    {
        private string _connectionString = string.Empty;

        public NivelAcademicoQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<NivelAcademicoResponseDto>> Listar(NivelAcademicoRequestDto request)
        {
            var rpta = new List<NivelAcademicoResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();

                var count = connection.QueryFirst<int>(
                   @"select count(ID_NIVEL_ACADEMICO) 'total'
                        from [dbo].[ods_nivel_academico]
                        where 1=1", parameter
                    );

                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [ID_NIVEL_ACADEMICO]
                          ,[DESCRIPCION]
                        from [dbo].[ods_nivel_academico]
                        where 1=1", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<NivelAcademicoResponseDto>(0, 0, count, rpta);
            }


        }

        private List<NivelAcademicoResponseDto> MapItems(dynamic result)
        {
            var lista = new List<NivelAcademicoResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new NivelAcademicoResponseDto
                {
                    IdNivelAcademico = item.ID_NIVEL_ACADEMICO,
                    Descripcion = item.DESCRIPCION
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
