using ApiBase.Contracts.Instituicao;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<SolicitacaoDto>>> Get()
        {
            IEnumerable<SolicitacaoDto> solicitacoes = await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value));

            if (!solicitacoes.Any())
            {
                return NotFound();
            }

            return Ok(solicitacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitacaoDto>> Get(int id)
        {
            return await _repository.GetSolicitacao(id, int.Parse(User.Claims.First(x => x.Type == "userId").Value));
        }

        // Para atualizar informações de uma solicitação de um usuário
        [HttpPut]
        public async Task<IActionResult> Put(SolicitacaoDto solicitaCurso)
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
