using ApiBase.Contracts;
using ApiBase.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.Visitante
{
    [Route("")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Produces("application/json")]
    public class VisitanteController(IVisitanteRepository repository) : ControllerBase
    {
        private readonly IVisitanteRepository _repository = repository;

        // Retorna todos os usuários
        /// <summary>
        /// Retorna todos os usuários com o tipo de conta "usuário"
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> Index()
        {
            IEnumerable<UsuarioDto> usuarios = await _repository.Index();

            if (!usuarios.Any())
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        /// <summary>
        /// Retorna o perfil de um usuário com o tipo de conta "usuário"
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<VisitanteDto>> Get(int userId)
        {
            VisitanteDto visitanteRetorno = await _repository.Get(userId);

            if (visitanteRetorno == null)
            {
                return NotFound();
            }

            return Ok(await _repository.Get(userId));
        }
    }
}
