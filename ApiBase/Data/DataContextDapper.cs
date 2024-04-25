using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiBase.Data
{
    public class DataContextDapper(IConfiguration config)
    {
        private readonly IConfiguration _config = config;

        public async Task<bool> ExecuteSqlWithParametersAsync(string sql, DynamicParameters parameters)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            return await dbConnection.ExecuteAsync(sql, parameters) > 0;
        }

        public async Task<T> LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            return await dbConnection.QuerySingleAsync<T>(sql);
        }

        public async Task<T> LoadDataSingleWithParametersAsync<T>(string sql, DynamicParameters parameters)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            return await dbConnection.QuerySingleAsync<T>(sql, parameters);
        }

        public async Task<IEnumerable<T>> LoadDataAsync<T>(string sql)
        {
            // 'IAsyncEnumerable<T>' executar operações assíncronas, como leitura de dados de bancos de dados ou outras operações que podem demorar mais tempo.

            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            return await dbConnection.QueryAsync<T>(sql);
        }
    }
}
