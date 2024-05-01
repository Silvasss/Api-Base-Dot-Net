using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace ApiBase.Controllers.UsuarioLogado
{
    [Authorize(Policy = "UsuarioOnly")]
    [Route("usuario/graduacao")]
    [ApiController]
    public class GraduacaoController(IGraduacaoRepository repository) : ControllerBase
    {
        private readonly IGraduacaoRepository _repository = repository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GraduacaoDto>>> Get()
        {
            IEnumerable<GraduacaoDto> graduacoes = await _repository.GetAll(int.Parse(User.Claims.First(x => x.Type == "userId").Value));

            if (!graduacoes.Any())
            {
                return NotFound();
            }

            return Ok(graduacoes);
        }

        [HttpPost]
        public async Task<IActionResult> Post(GraduacaoDto graduacao)
        {
            if (await _repository.Post(graduacao, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return Created();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na graduacao!",
                ContentType = "text/plain"
            };
        }

        [HttpPut]
        public async Task<IActionResult> Put(GraduacaoDto graduacao)
        {
            if (await _repository.Put(graduacao, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na graduacao!",
                ContentType = "text/plain"
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repository.Delete(id, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na solicitação!",
                ContentType = "text/plain"
            };
        }
    }
}
