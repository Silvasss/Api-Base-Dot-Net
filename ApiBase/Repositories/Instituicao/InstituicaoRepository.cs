using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Models;
using Dapper;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class InstituicaoRepository(IConfiguration config) : IInstituicaoRepository
    {
        private readonly DataContextDapper _dapper = new(config);

        public async Task<bool> Delete(int id)
        {
            string sql = @"EXEC spInstituicao_RequestDelete
                @Id = @IdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@IdParameter", id, DbType.Int32);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Put(InstituicaoModel instituicao, int id)
        {
            string sql = @"EXEC spInstituicao_PerfilUpdate
                @Id = @IdParameter,
                @Nome = @NomeParameter,  
                @PlusCode = @PlusCodeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@IdParameter", id, DbType.Int32);
            sqlParameters.Add("@NomeParameter", instituicao.Nome, DbType.String);
            sqlParameters.Add("@PlusCodeParameter", instituicao.PlusCode, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }
    }
}
