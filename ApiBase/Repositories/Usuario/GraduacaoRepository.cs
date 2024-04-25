using ApiBase.Contracts.Usuario;
using ApiBase.Data;
using ApiBase.Models;
using Dapper;
using System.Data;

namespace ApiBase.Repositories.Usuario
{
    public class GraduacaoRepository(IConfiguration config) : IGraduacaoRepository
    {
        private readonly DataContextDapper _dapper = new(config);

        public async Task<bool> Delete(int id)
        {
            string sql = @"EXEC spGraduacao_Delete 
                @UserId = @UserIdParameter,
                @GraduacaoId = @GraduacaoIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);
            sqlParameters.Add("@GraduacaoIdParameter", id, DbType.Int32);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Post(Graduacao graduacao, int id)
        {
            string sql = @"EXEC spGraduacao_Upsert
                @UserId = @UserIdParameter,                
                @GraduacaoId = @GraduacaoIdParameter,                 
                @InstituicaoId = @InstituicaoIdParameter, 
                @CursoId = @CursoIdParameter, 
                @Situacao = @SituacaoParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);
            sqlParameters.Add("@GraduacaoIdParameter", graduacao.Id, DbType.Int32);
            sqlParameters.Add("@InstituicaoIdParameter", graduacao.InstituicaoId, DbType.String);
            sqlParameters.Add("@CursoIdParameter", graduacao.CursoId, DbType.String);
            sqlParameters.Add("@SituacaoParameter", graduacao.Situacao, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Put(Graduacao graduacao, int id)
        {
            string sql = @"EXEC spGraduacao_Upsert
                @UserId = @UserIdParameter,                
                @GraduacaoId = @GraduacaoIdParameter,                 
                @InstituicaoId = @InstituicaoIdParameter, 
                @CursoId = @CursoIdParameter, 
                @Situacao = @SituacaoParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);
            sqlParameters.Add("@GraduacaoIdParameter", graduacao.Id, DbType.Int32);
            sqlParameters.Add("@InstituicaoIdParameter", graduacao.InstituicaoId, DbType.String);
            sqlParameters.Add("@CursoIdParameter", graduacao.CursoId, DbType.String);
            sqlParameters.Add("@SituacaoParameter", graduacao.Situacao, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }
    }
}
