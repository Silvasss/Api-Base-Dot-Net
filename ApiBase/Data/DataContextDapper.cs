using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;


namespace ApiBase.Data
{
    public class DataContextDapper(IConfiguration config)
    {
        private readonly IConfiguration _config = config;

        public bool ExecuteSqlWithParameters(string sql, DynamicParameters parameters)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            return dbConnection.Execute(sql, parameters) > 0;
        }
        public IEnumerable<T> LoadDataWithParameters<T>(string sql, DynamicParameters parameters)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            return dbConnection.Query<T>(sql, parameters);
        }

        public T LoadDataSingleWithParameters<T>(string sql, DynamicParameters parameters)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            return dbConnection.QuerySingle<T>(sql, parameters);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            return dbConnection.QuerySingle<T>(sql);
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            return dbConnection.Query<T>(sql);
        }
    }
}
