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
    [Route("api/v1/curso")]
    [ApiController]
    public class CursoController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);

        // Retorna os cursos da instituição 
        [HttpGet]
        public async Task<IEnumerable<Curso>> Get()
        {
            int id = int.Parse(User.Claims.First(x => x.Type == "userId").Value);

            return await _dapper.LoadDataAsync<Curso>(@"EXEC spInstituicao_CursosGet @InstituicaoId='" + id + "'");
        }

        // Inserir informações de um curso
        [HttpPost]
        public async Task<IActionResult> Post(Curso curso)
        {
            string sql = @"EXEC spInstituicao_Curso_Upsert
                @InstituicaoId = @UserIdParameter,
                @Nome = @NomeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@NomeParameter", curso.Nome, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return Created();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha na solicitação!",
                ContentType = "text/plain"
            };
        }

        // Atualizar informações de um curso
        [HttpPut]
        public async Task<IActionResult> Put(Curso curso)
        {
            string sql = @"EXEC spInstituicao_Curso_Upsert
                @InstituicaoId = @UserIdParameter,                   
                @CursoId = @CursoIdParameter,            
                @Nome = @NomeParameter,
                @IsActive = @IsActiveParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@CursoIdParameter", curso.Id, DbType.Int32);
            sqlParameters.Add("@NomeParameter", curso.Nome, DbType.String);
            sqlParameters.Add("@IsActiveParameter", curso.IsActive, DbType.Boolean);

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

        // Excluir permanentemente um curso
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string sql = @"EXEC spInstituicao_Curso_Delete 
                @InstituicaoId = @UserIdParameter,                   
                @CursoId = @CursoIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@CursoIdParameter", id, DbType.Int32);

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
