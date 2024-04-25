using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Helpers;
using ApiBase.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace ApiBase.Controllers.Usuario
{
    [Authorize(Policy = "UsuarioOnly")]
    [Route("api/v1/user")]
    [ApiController]
    public class UsuarioController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);
        private readonly AuthHelper _authHelper = new(config);

        // Retorna todos os dados criado pelo usuário
        [HttpGet]
        public async Task<ActionResult<UserCompleto>> Get()
        {
            int id = int.Parse(User.Claims.First(x => x.Type == "userId").Value);

            UserCompleto usuario = await _dapper.LoadDataSingle<UserCompleto>(@"EXEC spUser_Get @UserId='" + id + "'");

            usuario.Experiencia = (List<Experiencia>?) await _dapper.LoadDataAsync<Experiencia>(@"EXEC spExperiencia_Get @UserId='" + id + "'");

            usuario.Graduacao = (List<Graduacao>?) await _dapper.LoadDataAsync<Graduacao>(@"EXEC spGraduacao_Get @UserId='" + id + "'");

            return usuario;
        }

        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {
            string sql = @"EXEC spUser_PerfilUpdate
                @UserId = @UserIdParameter,
                @Nome = @NomeParameter,  
                @Pais = @PaisParameter,  
                @PlusCode = @PlusCodeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);
            sqlParameters.Add("@NomeParameter", user.Nome, DbType.String);
            sqlParameters.Add("@PaisParameter", user.Pais, DbType.String);
            sqlParameters.Add("@PlusCodeParameter", user.PlusCode, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return Ok();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha ao atualizar o usuário!",
                ContentType = "text/plain"
            };
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserForUpdatePassword updatePasswort)
        {
            UserForLoginDto userForSetPassword = new()
            {
                Usuario = User.Claims.First(x => x.Type == "nome").Value,
                Password = updatePasswort.PasswordNova
            };

            if (await _authHelper.UpdatePasswordAsync(userForSetPassword))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha ao atualizar a senha!",
                ContentType = "text/plain"
            };
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            string sql = @"EXEC spUser_Delete @UserId=@UserIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", int.Parse(User.Claims.First(x => x.Type == "userId").Value), DbType.Int32);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha na solicitação!",
                ContentType = "text/plain"
            };
        }
    }
}
