using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Dtos;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExperienciaDto>>> GetAll()
        {
            IEnumerable<ExperienciaDto> experiencias = await _repository.GetAll(int.Parse(User.Claims.First(x => x.Type == "userId").Value));

            if (!experiencias.Any())
            {
                return NotFound();
            }

            return Ok(experiencias);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExperienciaDto experiencia)
        {
            if (await _repository.Post(experiencia, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return Created();
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
                return NoContent();
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
