using ApiBase.Data;
using ApiBase.Models;
using Dapper;
using System.Data;


namespace ApiBase.Helpers
{
    public class ReusableSql(IConfiguration config)
    {
        private readonly DataContextDapper _dapper = new(config);

        public bool UpsertUser(UserComplete user)
        {
            // Essa primeira parte cria o novo usuário na tabela Users
            string sql = @"EXEC DotNetDatabase.spUser_Upsert
                @UserId = @UserIdParameter,
                @Email = @EmailParameter, 
                @Nome = @NomeParameter,  
                @Pais = @PaisParameter,  
                @Estado = @EstadoParameter,  
                @Cidade = @CidadeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", user.UserId, DbType.Int32);
            sqlParameters.Add("@EmailParameter", user.Email, DbType.String);
            sqlParameters.Add("@NomeParameter", user.Nome, DbType.String);
            sqlParameters.Add("@PaisParameter", user.Pais, DbType.String);
            sqlParameters.Add("@EstadoParameter", user.Estado, DbType.String);
            sqlParameters.Add("@CidadeParameter", user.Cidade, DbType.String);

            return _dapper.ExecuteSqlWithParameters(sql, sqlParameters);
        }
    }
}
