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
    public class TipoDocumentoQueries : ITipoDocumentoQueries
    {
        private string _connectionString = string.Empty;

        public TipoDocumentoQueries(string constr)
        {
            this._connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PaginatedItemsResponseViewModel<TipoDocumentoResponseDto>> Listar(TipoDocumentoRequestDto request)
        {
            var rpta = new List<TipoDocumentoResponseDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DynamicParameters parameter = new DynamicParameters();

                var count = connection.QueryFirst<int>(
                   @"select count(ID_TIPO_DOCUMENTO_IDENTIDAD) 'total'
                        from [dbo].[ods_tipo_documento_identidad]
                        where 1=1", parameter
                    );


                if (count > 0)
                {
                    var result = await connection.QueryAsync<dynamic>(
                   @"select [ID_TIPO_DOCUMENTO_IDENTIDAD]
                          ,[DESCRIPCION]
                        from [dbo].[ods_tipo_documento_identidad]
                        where 1=1", parameter
                    );

                    rpta = MapItems(result);
                }

                return new PaginatedItemsResponseViewModel<TipoDocumentoResponseDto>(0, 0, count, rpta);
            }


        }

        private List<TipoDocumentoResponseDto> MapItems(dynamic result)
        {
            var lista = new List<TipoDocumentoResponseDto>();

            foreach (dynamic item in result)
            {
                var temp = new TipoDocumentoResponseDto
                {
                    IdTipoDocumento = item.ID_TIPO_DOCUMENTO_IDENTIDAD,
                    Descripcion = item.DESCRIPCION
                };
                lista.Add(temp);
            }

            return lista;
        }

    }
}
