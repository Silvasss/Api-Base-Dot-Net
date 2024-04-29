using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Helpers;
using ApiBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace ApiBase.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    [ApiController]
    public class AuthController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextEF _entityFramework = new(config);
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
            Auth? dadosLogin = await _entityFramework.Auth.Where(a => a.Usuario == userForLogin.Usuario).FirstAsync<Auth>();
                                
            if (dadosLogin == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Credenciais inválidas");
            }
            
            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, dadosLogin.PasswordSalt);

            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != dadosLogin.PasswordHash[index])
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Credenciais inválidas");
                }
            }

            return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(dadosLogin.UsuarioPerfil.Usuario_Id, dadosLogin.UsuarioPerfil.TipoConta.Nome, userForLogin.Usuario)}
            });
        }
    }
}