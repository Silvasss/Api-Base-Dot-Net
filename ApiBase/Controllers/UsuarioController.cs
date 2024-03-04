using ApiBase.Data;
using ApiBase.Helpers;
using ApiBase.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;


namespace ApiBase.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);
        private readonly ReusableSql _reusableSql = new(config);


        // Informações do perfil
        [HttpGet("perfil")]
        public IEnumerable<UserComplete> GetPerfil()
        {
            string sql = @"EXEC DotNetDatabase.spUsuario_Get @UserId=@UserIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", this.User.FindFirst("UserId")?.Value, DbType.Int32);

            return _dapper.LoadDataWithParameters<UserComplete>(sql, sqlParameters);
        }

        // Atualização do perfil
        [HttpPut("upsertUser")]
        public IActionResult UpsertUser(UserComplete user)
        {
            user.UserId = Int32.Parse(this.HttpContext.User.FindFirst("userId")?.Value);

            if (_reusableSql.UpsertUser(user))
            {
                return Ok();
            }

            throw new Exception("Failed to Update User");
        }

        // Retorna todas as experiências profissionais do usuário
        [HttpGet("experiencia")]
        public IEnumerable<ExperienciaCompleto> GetExperiencia()
        {
            string sql = @"EXEC DotNetDatabase.spExperiencia_Get @UserId=@UserIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.String);

            return _dapper.LoadDataWithParameters<ExperienciaCompleto>(sql, sqlParameters);
        }

        // Atualização ou criação de experiências profissionais
        [HttpPut("upsertExperiencia")]
        public IActionResult UpsertExperiencia(ExperienciaCompleto experienciaCompleto)
        {
            experienciaCompleto.UserId = Int32.Parse(this.HttpContext.User.FindFirst("userId")?.Value);

            string sql = @"EXEC DotNetDatabase.spExperiencia_Upsert 
                @UserId=@UserIdParameter,                
                @ExperienciaId=@ExperienciaIdParameter,                 
                @Setor=@SetorParameter, 
                @NomeEmpresa=@NomeEmpresaParameter, 
                @Situacao=@SituacaoParameter,  
                @TipoEmprego=@TipoEmpregoParameter,
                @Pais=@PaisParameter,
                @Estado=@EstadoParameter,
                @Cidade=@CidadeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameters.Add("@ExperienciaIdParameter", experienciaCompleto.ExperienciaId, DbType.Int32);
            sqlParameters.Add("@SetorParameter", experienciaCompleto.Setor, DbType.String);
            sqlParameters.Add("@NomeEmpresaParameter", experienciaCompleto.NomeEmpresa, DbType.String);
            sqlParameters.Add("@SituacaoParameter", experienciaCompleto.Situacao, DbType.String);
            sqlParameters.Add("@TipoEmpregoParameter", experienciaCompleto.TipoEmprego, DbType.String);
            sqlParameters.Add("@PaisParameter", experienciaCompleto.Pais, DbType.String);
            sqlParameters.Add("@EstadoParameter", experienciaCompleto.Estado, DbType.String);
            sqlParameters.Add("@CidadeParameter", experienciaCompleto.Cidade, DbType.String);

            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
            {
                return Ok();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Failed to Update Experiência",
                ContentType = "text/plain"
            };
        }

        // Retorna todas as experiências profissionais do usuário
        [HttpGet("graduacao")]
        public IEnumerable<GraduacaoCompleto> GetGraduacao()
        {
            string sql = @"EXEC DotNetDatabase.spGraduacao_Get @UserId=@UserIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);

            return _dapper.LoadDataWithParameters<GraduacaoCompleto>(sql, sqlParameters);
        }

        // Atualização ou criação da graduação
        [HttpPut("upsertGraduacao")]
        public IActionResult UpsertGraduacao(GraduacaoCompleto graduacaoCompleto)
        {
            graduacaoCompleto.UserId = Int32.Parse(this.HttpContext.User.FindFirst("userId")?.Value);

            string sql = @"EXEC DotNetDatabase.spGraduacao_Upsert
                @UserId=@UserIdParameter,                
                @GraduacaoId=@GraduacaoIdParameter,                 
                @Instituicao=@InstituicaoParameter, 
                @Curso=@CursoParameter, 
                @Situacao=@SituacaoParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameters.Add("@GraduacaoIdParameter", graduacaoCompleto.GraduacaoId, DbType.Int32);
            sqlParameters.Add("@InstituicaoParameter", graduacaoCompleto.Instituicao, DbType.String);
            sqlParameters.Add("@CursoParameter", graduacaoCompleto.Curso, DbType.String);
            sqlParameters.Add("@SituacaoParameter", graduacaoCompleto.Situacao, DbType.String);

            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
            {
                return Ok();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Failed to Update Graduacao",
                ContentType = "text/plain"
            };
        }
    }
}
