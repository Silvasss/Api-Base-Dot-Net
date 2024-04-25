using ApiBase.Data;
using ApiBase.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace ApiBase.Controllers.Instituicao
{
    [Authorize(Policy = "InstituicaoOnly")]
    [Route("api/v1/solicitacao")]
    [ApiController]
    public class SolicitacaoController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);

        // Retorna as solicitações
        [HttpGet]
        public async Task<IEnumerable<SolicitaCurso>> Get()
        {
            int id = int.Parse(User.Claims.First(x => x.Type == "userId").Value);

            return await _dapper.LoadDataAsync<SolicitaCurso>(@"EXEC spInstituicao_SolicitacaoGet @InstituicaoId='" + id + "'");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitaCurso>> Get(int id)
        {
            string sql = @"EXEC spInstituicao_SolicitacaoGet 
                @InstituicaoId=@UserIdParameter,
                @SolicitacaoId=@SolicitacaoIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@SolicitacaoIdParameter", id, DbType.Int32);

            return await _dapper.LoadDataSingleWithParametersAsync<SolicitaCurso>(sql, sqlParameters);
        }

        // Para atualizar informações de uma solicitação de um usuário
        [HttpPut]
        public async Task<IActionResult> Put(SolicitaCurso solicitaCurso)
        {
            string sql = @"EXEC spInstituicao_Solicitacao_Update
                @InstituicaoId=@UserIdParameter,
                @SolicitacaoId = @SolicitacaoIdParameter,                
                @Situacao = @SituacaoParameter,                 
                @Explicacao = @ExplicacaoParameter, 
                @IsActive = @IsActiveParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@SolicitacaoIdParameter", solicitaCurso.Id, DbType.Int32);
            sqlParameters.Add("@SituacaoParameter", solicitaCurso.Situacao, DbType.String);
            sqlParameters.Add("@ExplicacaoParameter", solicitaCurso.Explicacao, DbType.String);
            sqlParameters.Add("@IsActiveParameter", solicitaCurso.IsActive, DbType.Boolean);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na experiência!",
                ContentType = "text/plain"
            };
        }
    }
}
