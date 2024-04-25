using ApiBase.Data;
using ApiBase.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace ApiBase.Controllers.Usuario
{
    [Authorize(Policy = "UsuarioOnly")]
    [Route("api/v1/graduacao")]
    [ApiController]
    public class GraduacaoController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);

        [HttpPost]
        public async Task<IActionResult> Post(Graduacao graduacao)
        {
            string sql = @"EXEC spGraduacao_Upsert
                @UserId = @UserIdParameter,                
                @GraduacaoId = @GraduacaoIdParameter,                 
                @InstituicaoId = @InstituicaoIdParameter, 
                @CursoId = @CursoIdParameter, 
                @Situacao = @SituacaoParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@GraduacaoIdParameter", graduacao.Id, DbType.Int32);
            sqlParameters.Add("@InstituicaoIdParameter", graduacao.InstituicaoId, DbType.String);
            sqlParameters.Add("@CursoIdParameter", graduacao.CursoId, DbType.String);
            sqlParameters.Add("@SituacaoParameter", graduacao.Situacao, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return Created();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na graduacao!",
                ContentType = "text/plain"
            };
        }

        [HttpPut]
        public async Task<IActionResult> Put(Graduacao graduacao)
        {
            string sql = @"EXEC spGraduacao_Upsert
                @UserId = @UserIdParameter,                
                @GraduacaoId = @GraduacaoIdParameter,                 
                @InstituicaoId = @InstituicaoIdParameter, 
                @CursoId = @CursoIdParameter, 
                @Situacao = @SituacaoParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@GraduacaoIdParameter", graduacao.Id, DbType.Int32);
            sqlParameters.Add("@InstituicaoIdParameter", graduacao.InstituicaoId, DbType.String);
            sqlParameters.Add("@CursoIdParameter", graduacao.CursoId, DbType.String);
            sqlParameters.Add("@SituacaoParameter", graduacao.Situacao, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na graduacao!",
                ContentType = "text/plain"
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string sql = @"EXEC spGraduacao_Delete 
                @UserId = @UserIdParameter,
                @GraduacaoId = @GraduacaoIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@GraduacaoIdParameter", id, DbType.Int32);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na solicitação!",
                ContentType = "text/plain"
            };
        }
    }
}
