using ApiBase.Contracts.Instituicao;
using ApiBase.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiBase.Controllers.Instituicao
{
    [Authorize(Policy = "InstituicaoOnly")]
    [Route("instituicao")]
    [ApiController]
    public class InstituicaoController(IInstituicaoRepository repository) : ControllerBase
    {
        private readonly IInstituicaoRepository _repository = repository;

        [HttpGet]
        public async Task<InstituicaoDto> Get()
        {
            return await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value));
        }

        // Atualizar as informações da instituição
        [HttpPut]
        public async Task<IActionResult> Put(InstituicaoDto instituicao)
        {
            if (await _repository.Put(instituicao, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha ao atualizar o usuário!",
                ContentType = "text/plain"
            };
        }

        // Solicitar excluir permanentemente seus dados
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            if (await _repository.Delete(int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha ao atualizar o usuário!",
                ContentType = "text/plain"
            };
        }
    }
}
