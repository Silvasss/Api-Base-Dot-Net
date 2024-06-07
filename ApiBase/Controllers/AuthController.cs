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
        ///         "Nome": "NomeDeExibição",
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
            if (await _authHelper.SetPasswordAsync(userForRegistration))
            {
                return Created();
            }

            return BadRequest();
        }

        /// <summary>
        /// Autenticação
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
        /// <response code="200">Token, tipo da conta e nome</response>
        /// <response code="401">Falha na validação da entrada</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            Auth? dadosLogin = await _entityFramework.Auth.Where(a => a.Usuario == userForLogin.Usuario).Include("UsuarioPerfil").FirstOrDefaultAsync();
            
            if (dadosLogin.UsuarioPerfil != null)
            {
                byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, dadosLogin.PasswordSalt);

                for (int index = 0; index < passwordHash.Length; index++)
                {
                    if (passwordHash[index] != dadosLogin.PasswordHash[index])
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized, "Credenciais inválidas");
                    }
                }

                TipoConta politicaConta = await _entityFramework.TipoContas.FindAsync(dadosLogin.UsuarioPerfil.Tipo_Conta_Id);

                return Ok(new
                {
                    token = _authHelper.CreateToken(dadosLogin.UsuarioPerfil.Usuario_Id, politicaConta.Nome, userForLogin.Usuario),
                    role = politicaConta.Nome,
                    nome = dadosLogin.UsuarioPerfil.Nome
                });
            }

            dadosLogin = await _entityFramework.Auth.Where(a => a.Usuario == userForLogin.Usuario).Include("Instituicao").FirstOrDefaultAsync();

            if (dadosLogin.Instituicao != null)
            {
                byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, dadosLogin.PasswordSalt);

                for (int index = 0; index < passwordHash.Length; index++)
                {
                    if (passwordHash[index] != dadosLogin.PasswordHash[index])
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized, "Credenciais inválidas");
                    }
                }

                TipoConta politicaConta = await _entityFramework.TipoContas.FindAsync(dadosLogin.Instituicao.Tipo_Conta_Id);

                return Ok(new
                {
                    token = _authHelper.CreateToken(dadosLogin.Instituicao.Instituicao_Id, politicaConta.Nome, userForLogin.Usuario),
                    role = politicaConta.Nome,
                    nome = dadosLogin.Instituicao.Nome
                });
            }

            return StatusCode(StatusCodes.Status401Unauthorized, "Credenciais inválidas");
        }
    }
}