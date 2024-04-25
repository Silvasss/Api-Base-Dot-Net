using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Models;
using Dapper;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class CursoRepository(IConfiguration config) : ICursoRepository
    {
        private readonly DataContextDapper _dapper = new(config);

        public async Task<bool> Delete(int cursoId, int id)
        {
            string sql = @"EXEC spInstituicao_Curso_Delete 
                @InstituicaoId = @UserIdParameter,                   
                @CursoId = @CursoIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);
            sqlParameters.Add("@CursoIdParameter", cursoId, DbType.Int32);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Curso>> Get(int id)
        {
            return await _dapper.LoadDataAsync<Curso>(@"EXEC spInstituicao_CursosGet @InstituicaoId='" + id + "'");
        }

        public async Task<bool> Post(Curso curso, int id)
        {
            string sql = @"EXEC spInstituicao_Curso_Upsert
                @InstituicaoId = @UserIdParameter,
                @Nome = @NomeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);
            sqlParameters.Add("@NomeParameter", curso.Nome, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Put(Curso curso, int id)
        {
            string sql = @"EXEC spInstituicao_Curso_Upsert
                @InstituicaoId = @UserIdParameter,                   
                @CursoId = @CursoIdParameter,            
                @Nome = @NomeParameter,
                @IsActive = @IsActiveParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);
            sqlParameters.Add("@CursoIdParameter", curso.Id, DbType.Int32);
            sqlParameters.Add("@NomeParameter", curso.Nome, DbType.String);
            sqlParameters.Add("@IsActiveParameter", curso.IsActive, DbType.Boolean);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }
    }
}
