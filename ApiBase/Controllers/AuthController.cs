using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Helpers;
using ApiBase.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;


namespace ApiBase.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);
        private readonly AuthHelper _authHelper = new(config);

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistrationDto userForRegistration)
        {
            UserForLoginDto userForSetPassword = new()
            {
                Usuario = userForRegistration.Usuario,
                Password = userForRegistration.Password
            };

            if (await _authHelper.SetPasswordAsync(userForSetPassword))
            {
                return Ok();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "O endereço de usuário já está em uso por outra conta",
                ContentType = "text/plain"
            };
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            string sqlForHashAndSalt = @"EXEC spLoginConfirmation_Get 
                @Usuario = @UsuarioParam";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UsuarioParam", userForLogin.Usuario, DbType.String);

            UserForLoginConfirmationDto userForConfirmation = await _dapper.LoadDataSingleWithParametersAsync<UserForLoginConfirmationDto>(sqlForHashAndSalt, sqlParameters);

            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, userForConfirmation.PasswordSalt);

            // if (passwordHash == userForConfirmation.PasswordHash) // Won't work

            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != userForConfirmation.PasswordHash[index])
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Credenciais inválidas");
                }
            }

            var sql = @"EXEC spUser_GetLogin
                @Usuario = @UsuarioParameter";

            sqlParameters.Add("@UsuarioParameter", userForLogin.Usuario, DbType.String);

            GetLogin dadosLogin = await _dapper.LoadDataSingleWithParametersAsync<GetLogin>(sql, sqlParameters);

            return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(dadosLogin.Id, dadosLogin.Role, userForLogin.Usuario)}
            });
        }
    }
}