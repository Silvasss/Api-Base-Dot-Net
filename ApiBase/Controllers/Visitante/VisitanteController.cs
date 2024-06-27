using ApiBase.Contracts;
using ApiBase.Dtos;
using ApiBase.Pagination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<ActionResult<IEnumerable<object>>> Index([FromQuery] VisitanteParameters visitanteParams)
        {
            var usuarios = await _repository.Index(visitanteParams);

            var metadata = new
            {
                usuarios.TotalCount,
                usuarios.PageSize,
                usuarios.CurrentPage,
                usuarios.TotalPages,
                usuarios.HasNext,
                usuarios.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            if (usuarios is null || !usuarios.Any())
            {
                return NotFound();
            }

            return usuarios.Select(user => new 
            {
                user.Usuario_Id,
                user.Nome,
                user.Pais,
                user.PlusCode,
                user.SobreMin,
                user.CargoPrincipal
            }).ToList();
        }

        /// <summary>
        /// Retorna o perfil de um usuário com o tipo de conta "usuário"
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<object>> Get(int userId)
        {
            var visitanteRetorno = await _repository.Get(userId);

            if (visitanteRetorno == null)
            {
                return NotFound();
            }

            return Ok(visitanteRetorno);
        }
    }
}
