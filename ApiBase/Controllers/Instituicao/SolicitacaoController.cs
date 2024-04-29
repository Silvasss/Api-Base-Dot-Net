using ApiBase.Contracts.Instituicao;
using ApiBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiBase.Controllers.Instituicao
{
    [Authorize(Policy = "InstituicaoOnly")]
    [Route("instituicao/solicitacao")]
    [ApiController]
    public class SolicitacaoController(ISoliciticacoesRepository repository) : ControllerBase
    {
        private readonly ISoliciticacoesRepository _repository = repository;

        // Retorna as solicitações
        [HttpGet]
        public async Task<IEnumerable<Solicitacao>> Get()
        {
            return await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitacao>> Get(int id)
        {
            return await _repository.GetSolicitacao(id, int.Parse(User.Claims.First(x => x.Type == "userId").Value));
        }

        // Para atualizar informações de uma solicitação de um usuário
        [HttpPut]
        public async Task<IActionResult> Put(Solicitacao solicitaCurso)
        {
            if (await _repository.Put(solicitaCurso, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na experiência!",
                ContentType = "text/plain"
            };
        }
    }
}
