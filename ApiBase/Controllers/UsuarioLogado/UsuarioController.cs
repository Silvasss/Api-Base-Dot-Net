using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.UsuarioLogado
{
    [Authorize(Policy = "UsuarioOnly")]
    [Route("usuario")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController(IUsuarioRepository repository) : ControllerBase
    {
        private readonly IUsuarioRepository _repository = repository;

        /// <summary>
        /// Retorna as informações geral do perfil
        /// </summary>
        /// <response code="200">Objeto</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UsuarioIndexDto>> Get()
        {
            return Ok(await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value)));
        }

        /// <summary>
        /// Atualizar as informações do perfil
        /// </summary>
        /// <remarks>        
        /// Exemplo de request:     
        /// 
        ///     {
        ///         "Nome": "NomeDoUsuario",
        ///         "Pais": "Brasil",
        ///         "PlusCode": "RM78+7G Plano Diretor Sul, Palmas - State of Tocantins"
        ///     }
        /// Os Plus Codes funcionam como endereços físicos. São baseados em latitude e longitude e usam um 
        /// sistema de grade simples e um conjunto de 20 caracteres alfanuméricos.   
        /// </remarks>
        /// <param name="user">Objeto Usuario</param>
        /// <response code="204">Atualizado</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(UsuarioDto user)
        {
            user.Usuario_Id = int.Parse(User.Claims.First(x => x.Type == "userId").Value);

            await _repository.Put(user);

            return NoContent();
        }

        /// <summary>
        /// Apagar a conta 
        /// </summary>
        /// <response code="204">Apagado</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete()
        {
            await _repository.Delete(int.Parse(User.Claims.First(x => x.Type == "userId").Value));

            return NoContent();
        }

        /// <summary>
        /// Retorna as configurações da conta
        /// </summary>
        /// <response code="200">Objeto</response>
        [HttpGet("configuracoes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UsuarioDto>> Get2()
        {
            return Ok(await _repository.GetConfiguracoes(int.Parse(User.Claims.First(x => x.Type == "userId").Value)));
        }

        /// <summary>
        /// Atualizar as configurações da conta
        /// </summary>
        /// <remarks>        
        /// Exemplo de request:     
        /// 
        ///     {
        ///         "Configuracoes": ['1', '2']
        ///     } 
        /// </remarks>
        /// <param name="temp">Lista de string</param>
        /// <response code="204">Atualizado</response>
        [HttpPut("configuracoes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put2(List<string> temp)
        {
            await _repository.PutConfiguracoes(temp, int.Parse(User.Claims.First(x => x.Type == "userId").Value));
                
            return NoContent();
        }
    }
}
