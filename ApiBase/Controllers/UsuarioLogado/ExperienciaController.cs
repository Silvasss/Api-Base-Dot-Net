using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiBase.Controllers.UsuarioLogado
{
    [Authorize(Policy = "UsuarioOnly")]
    [Route("usuario/experiencia")]
    [ApiController]
    public class ExperienciaController(IExperienciaRepository repository) : ControllerBase
    {
        private readonly IExperienciaRepository _repository = repository;

        [HttpPost]
        public async Task<IActionResult> Post(ExperienciaDto experiencia)
        {
            if (await _repository.Post(experiencia, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return Ok();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha na experiência!",
                ContentType = "text/plain"
            };
        }

        [HttpPut]
        public async Task<IActionResult> Put(ExperienciaDto experiencia)
        {
            if (await _repository.Put(experiencia, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return Ok();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha na experiência!",
                ContentType = "text/plain"
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repository.Delete(int.Parse(User.Claims.First(x => x.Type == "userId").Value), id))
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
