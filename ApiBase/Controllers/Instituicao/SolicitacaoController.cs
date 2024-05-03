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
        /// Retorna lista de solicitações de graduações que não foram validadas
        /// </summary>
        /// <response code="200">Objetos solicitação</response>
        /// <response code="404">Nenhuma encontrada</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<SolicitacaoDto>>> Get()
        {
            IEnumerable<SolicitacaoDto> solicitacoes = await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value));

            if (!solicitacoes.Any())
            {
                return NotFound();
            }

            return Ok(solicitacoes);
        }

        /// <summary>
        /// Retorna uma solicitação
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Solicitação</response>
        /// <response code="404">Não encontrada</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitacaoDto>> Get(int id)
        {
            SolicitacaoDto solicitacao = await _repository.GetSolicitacao(id, int.Parse(User.Claims.First(x => x.Type == "userId").Value));
            
            if (solicitacao != null)
            {
                return solicitacao;
            }

            return NotFound();
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
        /// <param name="solicitaCurso">Objeto Solicitação</param>
        /// <response code="204">Atualizada</response>
        /// <response code="404">Não encontrada</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(SolicitacaoDto solicitaCurso)
        {
            if (await _repository.Put(solicitaCurso, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
