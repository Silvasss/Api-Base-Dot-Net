using ApiBase.Contracts.Instituicao;
using ApiBase.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiBase.Controllers.Instituicao
{
    [Authorize(Policy = "InstituicaoOnly")]
    [Route("instituicao/curso")]
    [ApiController]
    public class CursoController(ICursoRepository repository) : ControllerBase
    {
        private readonly ICursoRepository _repository = repository;

        // Retorna os cursos da instituição 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoDto>>> Get()
        {
            IEnumerable<CursoDto> curso = await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value));

            if (!curso.Any())
            {
                return NotFound();
            }

            return Ok(curso);
        }

        // Inserir informações de um curso
        [HttpPost]
        public async Task<IActionResult> Post(CursoDto curso)
        {
            if (await _repository.Post(curso, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return Created();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha na solicitação!",
                ContentType = "text/plain"
            };
        }

        // Atualizar informações de um curso
        [HttpPut]
        public async Task<IActionResult> Put(CursoDto curso)
        {
            if (await _repository.Put(curso, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
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

        // Excluir permanentemente um curso
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
