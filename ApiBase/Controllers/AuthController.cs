using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Helpers;
using ApiBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Controllers
{
    [AllowAnonymous]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("auth")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextEF _entityFramework = new(config);
        private readonly AuthHelper _authHelper = new(config);

        /// <summary>
        /// Registrar um novo usuário
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     {
        ///         "Usuario": "NomeDoUsuario",
        ///         "Password": "SenhaDoUsuario"
        ///     }
        /// </remarks>
        /// <param name="userForRegistration">objeto UserForRegistrationDto</param>    
        /// <response code="201">Criado novo usuário</response>
        /// <response code="400">Falha na validação da entrada</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register(UserForRegistrationDto userForRegistration)
        {
            UserForLoginDto userForSetPassword = new()
            {
                Usuario = userForRegistration.Usuario,
                Password = userForRegistration.Password
            };

            if (await _authHelper.SetPasswordAsync(userForSetPassword))
            {
                return Created();
            }

            return BadRequest();
        }

        /// <summary>
        /// Autenticação para contas do tipo usuário e administrador 
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     {
        ///         "Usuario": "NomeDoUsuario",
        ///         "Password": "SenhaDoUsuario"
        ///     }
        /// </remarks>
        /// <param name="userForLogin">Objeto UserForLoginDto</param>
        /// <response code="200">Token do usuário</response>
        /// <response code="401">Falha na validação da entrada</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            Auth? dadosLogin = await _entityFramework.Auth.Where(a => a.Usuario == userForLogin.Usuario).Include("UsuarioPerfil").FirstOrDefaultAsync();

            if (dadosLogin == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Credenciais inválidas");
            }

            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, dadosLogin.PasswordSalt);

            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != dadosLogin.PasswordHash[index])
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Credenciais inválidas");
                }
            }

            TipoConta politicaConta = await _entityFramework.TipoContas.FindAsync(dadosLogin.UsuarioPerfil.Tipo_Conta_Id);

            return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(dadosLogin.UsuarioPerfil.Usuario_Id, politicaConta.Nome, userForLogin.Usuario)}
            });
        }

        /// <summary>
        /// Autenticação para contas do tipo instituição
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     {
        ///         "Usuario": "NomeDaInstituição",
        ///         "Password": "SenhaDaInstituição"
        ///     }
        /// </remarks>
        /// <param name="userForLogin">Objeto UserForLoginDto</param>
        /// <response code="200">Token do usuário</response>
        /// <response code="401">Falha na validação da entrada</response>
        [HttpPost("login2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login2(UserForLoginDto userForLogin)
        {
            Auth? dadosLogin = await _entityFramework.Auth.Where(a => a.Usuario == userForLogin.Usuario).Include("Instituicao").FirstOrDefaultAsync();

            if (dadosLogin == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Credenciais inválidas");
            }

            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, dadosLogin.PasswordSalt);

            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != dadosLogin.PasswordHash[index])
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Credenciais inválidas");
                }
            }

            TipoConta politicaConta = await _entityFramework.TipoContas.FindAsync(dadosLogin.Instituicao.Tipo_Conta_Id);

            return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(dadosLogin.Instituicao.Instituicao_Id, politicaConta.Nome, userForLogin.Usuario)}
            });
        }
    }
}