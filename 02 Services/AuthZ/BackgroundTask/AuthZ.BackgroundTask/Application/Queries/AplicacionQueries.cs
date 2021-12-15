using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuthZ.BackgroundTask.Application.ViewModels.AplicacionViewModel;
using Dapper;

namespace AuthZ.BackgroundTask.Application.Queries
{
    public class AplicacionQueries : IAplicacionQueries
    {

        private string _connectionString = string.Empty;

        public AplicacionQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<AplicacionViewModel>> ObtenerAplicaciones()
        {
            var result = new List<AplicacionViewModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                DynamicParameters parameter = new DynamicParameters();

                string query = @"SELECT [GUID],[ID_SISTEMA],[NOMBRE],[ABREVIATURA] FROM [maestro].[sistema] WHERE [ELIMINADO]=0";
                var resultQuery = await connection.QueryAsync<dynamic>(query, parameter, null, null, CommandType.Text);
                foreach (var item in resultQuery)
                {
                    var docenteRngt = new AplicacionViewModel()
                    {
                        IdSistema = item.GUID,
                        Codigo = item.ID_SISTEMA,
                        Nombre = item.NOMBRE,
                        Abreviatura = item.ABREVIATURA
                    };
                    result.Add(docenteRngt);
                }
            }
            return result;
        }
    }
}
