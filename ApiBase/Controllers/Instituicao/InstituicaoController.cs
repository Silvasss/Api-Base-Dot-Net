using ApiBase.Contracts.Instituicao;
using ApiBase.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.Instituicao
{
    [Authorize(Policy = "InstituicaoOnly")]
    [Route("instituicao")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Produces("application/json")]
    public class InstituicaoController(IInstituicaoRepository repository) : ControllerBase
    {
        private readonly IInstituicaoRepository _repository = repository;

        /// <summary>
        /// Retorna as informações do perfil
        /// </summary>
        /// <response code="200">Objeto Instituição</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<object>> Get()
        {
            return await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value));
        }

        /// <summary>
        /// Atualizar as informações do perfil
        /// </summary>
        /// <remarks>        
        /// Exemplo de request:     
        /// 
        ///     {
        ///         "Nome": "NomeDaInstituição",
        ///         "PlusCode": "RM78+7G Plano Diretor Sul, Palmas - State of Tocantins"
        ///     }
        /// Os Plus Codes funcionam como endereços físicos. São baseados em latitude e longitude e usam um 
        /// sistema de grade simples e um conjunto de 20 caracteres alfanuméricos.   
        /// </remarks>+
        /// <param name="instituicao">Objeto Instituição</param>
        /// <response code="204">Atualizado</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(InstituicaoDto instituicao)
        {
            await _repository.Put(instituicao, int.Parse(User.Claims.First(x => x.Type == "userId").Value));

            return NoContent();
        }
    }
}
