using ApiBase.Contracts.Instituicao;
using ApiBase.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.Instituicao
{
    [Authorize(Policy = "InstituicaoOnly")]
    [Route("instituicao/solicitacao")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Produces("application/json")]
    public class SolicitacaoController(ISoliciticacoesRepository repository) : ControllerBase
    {
        private readonly ISoliciticacoesRepository _repository = repository;

        /// <summary>
        /// Retorna os dados do dashboard
        /// </summary>
        /// <response code="200">Valores</response>
        [HttpGet]
        public async Task<ActionResult<object>> Get3()
        {
            return Ok(await _repository.Dashboard(int.Parse(User.Claims.First(x => x.Type == "userId").Value)));
        }

        /// <summary>
        /// Retorna lista de solicitações de graduações que não foram validadas
        /// </summary>
        /// <param name="status">Filtro de seleção</param>
        /// <response code="200">Objetos solicitação</response>
        [HttpGet("index/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> Get(int status)
        {
            return Ok(await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value), status));
        }

        /// <summary>
        /// Retorna uma solicitação
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Solicitação</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get2(int id)
        {
            return Ok(await _repository.GetSolicitacao(id));
        }

        /// <summary>
        /// Atualizar uma solicitação
        /// </summary>
        /// <remarks>              
        /// Exemplo de request:        
        /// 
        ///     {
        ///         "Solicitacao_Id": 1,
        ///         "Descricao": "DescriçãoDaSolicitação",
        ///         "Ativo": false
        ///     }  
        /// </remarks>
        /// <param name="request">Objeto Solicitação</param>
        /// <response code="204">Atualizada</response>
        /// <response code="404">Não encontrada</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(SolicitacaoDto request)
        {
            if (await _repository.Put(request, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
