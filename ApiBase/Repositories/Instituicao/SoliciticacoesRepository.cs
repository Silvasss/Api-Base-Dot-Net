using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class SoliciticacoesRepository(IConfiguration config) : ISoliciticacoesRepository
    {
        private readonly DataContextDapper _dapper = new(config);

        public async Task<IEnumerable<SolicitaCurso>> Get(int id)
        {
            return await _dapper.LoadDataAsync<SolicitaCurso>(@"EXEC spInstituicao_SolicitacaoGet @InstituicaoId='" + id + "'");
        }

        public async Task<SolicitaCurso> GetSolicitacao(int id, int InstituicaoId)
        {
            string sql = @"EXEC spInstituicao_SolicitacaoGet 
                @InstituicaoId=@UserIdParameter,
                @SolicitacaoId=@SolicitacaoIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", InstituicaoId, DbType.Int32);
            sqlParameters.Add("@SolicitacaoIdParameter", id, DbType.Int32);

            return await _dapper.LoadDataSingleWithParametersAsync<SolicitaCurso>(sql, sqlParameters);
        }

        public async Task<bool> Put(SolicitaCurso solicitaCurso, int id)
        {
            string sql = @"EXEC spInstituicao_Solicitacao_Update
                @InstituicaoId=@UserIdParameter,
                @SolicitacaoId = @SolicitacaoIdParameter,                
                @Situacao = @SituacaoParameter,                 
                @Explicacao = @ExplicacaoParameter, 
                @IsActive = @IsActiveParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);
            sqlParameters.Add("@SolicitacaoIdParameter", solicitaCurso.Id, DbType.Int32);
            sqlParameters.Add("@SituacaoParameter", solicitaCurso.Situacao, DbType.String);
            sqlParameters.Add("@ExplicacaoParameter", solicitaCurso.Explicacao, DbType.String);
            sqlParameters.Add("@IsActiveParameter", solicitaCurso.IsActive, DbType.Boolean);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }
    }
}
