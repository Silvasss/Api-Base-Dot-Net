using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.UsuarioLogado
{
    [Authorize(Policy = "UsuarioOnly")]
    [Route("usuario/graduacao")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Produces("application/json")]
    public class GraduacaoController(IGraduacaoRepository repository) : ControllerBase
    {
        private readonly IGraduacaoRepository _repository = repository;

        /// <summary>
        /// Lista de informações
        /// </summary>
        /// <response code="200">Objeto</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<object>> Get()
        {
            return Ok(await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value)));
        }

        /// <summary>
        /// Informações da solicitação
        /// </summary>
        /// <response code="200">Objeto</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<object>> Get2(int id)
        {
            return Ok(await _repository.GetSolicitacao(id));
        }

        /// <summary>
        /// Criar uma nova graduação
        /// </summary>
        /// <remarks>              
        /// Exemplo de request:        
        /// 
        ///     {
        ///         "Graduacao_Id": 1,
        ///         "Situacao": "Cursando",
        ///         "Inicio": "2024-05-02T17:03:49.182Z",
        ///         "Fim": "2024-05-02T17:03:49.182Z",
        ///         "curso_Id": 1,
        ///         "instituicaoId": 1
        ///     }  
        /// O campo 'Fim' é opcional.
        /// </remarks>
        /// <param name="graduacao"></param>
        /// <response code="201">Criada</response>
        /// <response code="400">Falha na validação da entrada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(GraduacaoDto graduacao)
        {
            await _repository.Post(graduacao, int.Parse(User.Claims.First(x => x.Type == "userId").Value));

            return Created();
        }

        /// <summary>
        /// Atualizar as informações de uma graduação
        /// </summary>
        /// <remarks>  
        /// Exemplo de request:        
        /// 
        ///     {
        ///         "Graduacao_Id": 1,
        ///         "Situacao": "Cursando",
        ///         "Inicio": "2024-05-02T17:03:49.182Z",
        ///         "Fim": "2024-05-02T17:03:49.182Z"
        ///     }  
        /// Não é possível alterar o curso é a instituição vinculada a um objeto. O campo 'Fim' é opcional.    
        /// </remarks>
        /// <param name="graduacao"></param>
        /// <response code="204">Atualizada</response>
        /// <response code="404">Não encontrada</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(GraduacaoDto graduacao)
        {
            if (await _repository.Put(graduacao, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return NotFound();
        }

        /// <summary>
        /// Apagar uma graduação
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Apagada</response>
        /// <response code="404">Não encotrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repository.Delete(id, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
