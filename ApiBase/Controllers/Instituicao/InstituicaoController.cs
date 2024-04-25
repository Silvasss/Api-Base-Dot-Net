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
    [Route("api/v1/instituicao")]
    [ApiController]
    public class InstituicaoController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);

        // Atualizar as informações da instituição
        [HttpPut]
        public async Task<IActionResult> Put(InstituicaoModel instituicao)
        {
            string sql = @"EXEC spInstituicao_PerfilUpdate
                @Id = @IdParameter,
                @Nome = @NomeParameter,  
                @PlusCode = @PlusCodeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@IdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@NomeParameter", instituicao.Nome, DbType.String);
            sqlParameters.Add("@PlusCodeParameter", instituicao.PlusCode, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha ao atualizar o usuário!",
                ContentType = "text/plain"
            };
        }

        // Solicitar excluir permanentemente seus dados
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            string sql = @"EXEC spInstituicao_RequestDelete
                @Id = @IdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@IdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
                 
            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha ao atualizar o usuário!",
                ContentType = "text/plain"
            };
        }
    }
}
