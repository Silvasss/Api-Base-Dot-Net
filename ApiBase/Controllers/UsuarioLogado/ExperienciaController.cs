using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.UsuarioLogado
{
    [Authorize(Policy = "UsuarioOnly")]
    [Route("usuario/experiencia")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Produces("application/json")]
    public class ExperienciaController(IExperienciaRepository repository) : ControllerBase
    {
        private readonly IExperienciaRepository _repository = repository;

        /// <summary>
        /// Retorna lista de experiências profissionais  
        /// </summary>
        /// <response code="200">Lista de objetos Experiencia</response>
        /// <response code="404">Nenhum objeto Experiencia encontrado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<ExperienciaDto>>> GetAll()
        {
            IEnumerable<ExperienciaDto> experiencias = await _repository.GetAll(int.Parse(User.Claims.First(x => x.Type == "userId").Value));

            if (!experiencias.Any())
            {
                return NotFound();
            }

            return Ok(experiencias);
        }

        /// <summary>
        /// Criar uma nova experiência profissional
        /// </summary>
        /// <remarks>              
        /// Exemplo de request:        
        /// 
        ///     {
        ///         "Setor": "Indústrial",
        ///         "Empresa": "Sorerf",
        ///         "PlusCode": "RM78+7G Plano Diretor Sul, Palmas - State of Tocantins",
        ///         "Vinculo": "CLT",
        ///         "Ativo": false,
        ///         "Inicio": "2016-01-12T17:03:49.182Z",
        ///         "Fim": "2024-05-02T17:03:49.182Z"
        ///     }  
        /// O campo 'Fim' é opcional quando o valor "Ativo" é 'true'.
        /// Os Plus Codes funcionam como endereços físicos. São baseados em latitude e longitude e usam um 
        /// sistema de grade simples e um conjunto de 20 caracteres alfanuméricos.  
        /// </remarks>
        /// <param name="experiencia">Objeto Experiencia</param>
        /// <response code="201">Nova experiencia profissional criada</response>
        /// <response code="500">Error interno do servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post(ExperienciaDto experiencia)
        {
            if (await _repository.Post(experiencia, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return Created();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Atualizar uma experiência profissional
        /// </summary>
        /// <remarks>              
        /// Exemplo de request:        
        /// 
        ///     {
        ///         "Experiencia_Id": 1,
        ///         "Setor": "Indústrial",
        ///         "Empresa": "Sorerf",
        ///         "PlusCode": "RM78+7G Plano Diretor Sul, Palmas - State of Tocantins",
        ///         "Vinculo": "CLT",
        ///         "Ativo": true,
        ///         "Inicio": "2016-01-12T17:03:49.182Z",
        ///         "Fim": null
        ///     }  
        /// O campo 'Fim' é opcional quando o valor "Ativo" é 'true'.
        /// Os Plus Codes funcionam como endereços físicos. São baseados em latitude e longitude e usam um 
        /// sistema de grade simples e um conjunto de 20 caracteres alfanuméricos.  
        /// </remarks>
        /// <param name="experiencia">Objeto Experiencia</param>
        /// <response code="204">Experiencia profissional atualizada</response>
        /// <response code="500">Error interno do servidor</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(ExperienciaDto experiencia)
        {
            if (await _repository.Put(experiencia, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Apagar uma experiência profissional
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Experiencia profissional apagada</response>
        /// <response code="404">Experiencia profissional não encotrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repository.Delete(int.Parse(User.Claims.First(x => x.Type == "userId").Value), id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
