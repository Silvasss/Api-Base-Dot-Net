using ApiBase.Data;
using ApiBase.Helpers;
using ApiBase.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Cryptography;

namespace ApiBase.Controllers.Administrado
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/v1/admin")]
    [ApiController]
    public class AdminController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);
        private readonly AuthHelper _authHelper = new(config);

        // Retorna os logs de alterações de contas da tabelas 'AuditLogs'
        [HttpGet]
        public Task<IEnumerable<AuditLogs>> Index()
        {
            return _dapper.LoadDataAsync<AuditLogs>(@"EXEC spAdmin_LogGet");
        }

        // Para inserir uma conta do tipo 'instituição'
        [HttpPost]
        public async Task<IActionResult> Post(InstituicaoInsert instituicaoInsert)
        {
            byte[] passwordSalt = new byte[128 / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);
            }

            byte[] passwordHash = _authHelper.GetPasswordHash(instituicaoInsert.Password, passwordSalt);

            string sql = @"EXEC spAdmin_InstituicaoInsert
                @Usuario = @UsuarioParameter,                                          
                @PasswordHash = @PasswordHashParameter,                
                @PasswordSalt = @PasswordSaltParameter,
                @Nome = @NomeParameter,
                @PlusCode = @PlusCodeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UsuarioParameter", instituicaoInsert.Usuario, DbType.String);
            sqlParameters.Add("@PasswordHashParameter", passwordHash, DbType.Binary);
            sqlParameters.Add("@PasswordSaltParameter", passwordSalt, DbType.Binary);
            sqlParameters.Add("@NomeParameter", instituicaoInsert.Nome, DbType.String);
            sqlParameters.Add("@PlusCodeParameter", instituicaoInsert.PlusCode, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha ao atualizar!",
                ContentType = "text/plain"
            };
        }

        // Alterar o nivel de um usuário
        [HttpPut("{userId}&{roleId}")]
        public async Task<IActionResult> Put(int userId, int roleId)
        {
            string sql = @"EXEC spAdmin_UserUpdateRole
                @UserId = @UserIdParameter,
                @Role = @RoleParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);
            sqlParameters.Add("@RoleParameter", roleId, DbType.Int32);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha ao atualizar!",
                ContentType = "text/plain"
            };
        }

        // Excluir uma conta
        [HttpDelete("/usuario/{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            string sql = @"EXEC spUser_Delete @UserId=@UserIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);

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
