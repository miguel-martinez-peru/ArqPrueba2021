using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AuthZ.BackgroundTask.Application.ViewModels.RolViewModel;
using Dapper;

namespace AuthZ.BackgroundTask.Application.Queries
{
    public class RolQueries : IRolQueries
    {

        private string _connectionString = string.Empty;

        public RolQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<RolViewModel>> ObtenerRoles()
        {
            var result = new List<RolViewModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                DynamicParameters parameter = new DynamicParameters();

                string query = @"SELECT [GUID],[ID_SISTEMA],[NOMBRE] FROM [maestro].[rol] WHERE [ELIMINADO]=0";
                var resultQuery = await connection.QueryAsync<dynamic>(query, parameter, null, null, CommandType.Text);
                foreach (var item in resultQuery)
                {
                    var docenteRngt = new RolViewModel()
                    {
                        IdRol = item.GUID,
                        IdSistema = item.ID_SISTEMA,
                        Nombre = item.NOMBRE
                    };
                    result.Add(docenteRngt);
                }
            }
            return result;
        }
    }
}
