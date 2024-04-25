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
    [Route("api/v1/experiencia")]
    [ApiController]
    public class ExperienciaController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);

        [HttpPost]
        public async Task<IActionResult> Post(Experiencia experiencia)
        {
            string sql = @"EXEC spExperiencia_Upsert
                @UserId = @UserIdParameter,                
                @ExperienciaId = @ExperienciaIdParameter,                 
                @Setor = @SetorParameter, 
                @Empresa = @EmpresaParameter, 
                @Situacao = @SituacaoParameter,  
                @Tipo = @TipoParameter,
                @PlusCode = @PlusCodeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@ExperienciaIdParameter", experiencia.Id, DbType.Int32);
            sqlParameters.Add("@SetorParameter", experiencia.Setor, DbType.String);
            sqlParameters.Add("@EmpresaParameter", experiencia.Empresa, DbType.String);
            sqlParameters.Add("@SituacaoParameter", experiencia.Situacao, DbType.String);
            sqlParameters.Add("@TipoParameter", experiencia.Tipo, DbType.String);
            sqlParameters.Add("@PlusCodeParameter", experiencia.PlusCode, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return Ok();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha na experiência!",
                ContentType = "text/plain"
            };
        }

        [HttpPut]
        public async Task<IActionResult> Put(Experiencia experiencia)
        {
            string sql = @"EXEC spExperiencia_Upsert
                @UserId = @UserIdParameter,                
                @ExperienciaId = @ExperienciaIdParameter,                 
                @Setor = @SetorParameter, 
                @Empresa = @EmpresaParameter, 
                @Situacao = @SituacaoParameter,  
                @Tipo = @TipoParameter,
                @PlusCode = @PlusCodeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@ExperienciaIdParameter", experiencia.Id, DbType.Int32);
            sqlParameters.Add("@SetorParameter", experiencia.Setor, DbType.String);
            sqlParameters.Add("@EmpresaParameter", experiencia.Empresa, DbType.String);
            sqlParameters.Add("@SituacaoParameter", experiencia.Situacao, DbType.String);
            sqlParameters.Add("@TipoParameter", experiencia.Tipo, DbType.String);
            sqlParameters.Add("@PlusCodeParameter", experiencia.PlusCode, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return Ok();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha na experiência!",
                ContentType = "text/plain"
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string sql = @"EXEC spExperiencia_Delete 
                @UserId = @UserIdParameter,
                @ExperienciaId = @ExperienciaIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@ExperienciaIdParameter", id, DbType.Int32);

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
