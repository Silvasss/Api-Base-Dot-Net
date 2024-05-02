using ApiBase.Contracts.Instituicao;
using ApiBase.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.Instituicao
{
    [Authorize(Policy = "InstituicaoOnly")]
    [Route("instituicao/curso")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Produces("application/json")]
    public class CursoController(ICursoRepository repository) : ControllerBase
    {
        private readonly ICursoRepository _repository = repository;

        // ------------------------------------------------------
        // Implementar função que retorna todas as graduações validadas de um curso
        // ------------------------------------------------------

        /// <summary>
        /// Retorna lista de cursos
        /// </summary>
        /// <response code="200">Lista de objetos Curso</response>
        /// <response code="404">Nenhum objeto Curso encontrado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<CursoDto>>> Get()
        {
            IEnumerable<CursoDto> curso = await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value));

            if (!curso.Any())
            {
                return NotFound();
            }

            return Ok(curso);
        }

        /// <summary>
        /// Criar um novo curso
        /// </summary>
        /// <remarks>              
        /// Exemplo de request:        
        /// 
        ///     {
        ///         "Nome": "NomeDoCurso",
        ///         "Ativo": true
        ///     }  
        /// </remarks>
        /// <param name="curso">Objeto Curso</param>
        /// <response code="201">Novo curso criado</response>
        /// <response code="500">Error interno do servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post(CursoDto curso)
        {
            if (await _repository.Post(curso, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return Created();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Atualizar informações de um curso
        /// </summary>
        /// <remarks>              
        /// Exemplo de request:        
        /// 
        ///     {
        ///         "Curso_Id": 1,
        ///         "Nome": "NomeDoCurso",
        ///         "Ativo": false
        ///     }  
        /// </remarks>
        /// <param name="curso">Objeto Curso</param>
        /// <response code="204">Curso atualizado</response>
        /// <response code="404">Curso não encotrado</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(CursoDto curso)
        {
            if (await _repository.Put(curso, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return NotFound();
        }

        /// <summary>
        /// Apagar um curso
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Curso apagado</response>
        /// <response code="404">Curso não encotrado</response>
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
