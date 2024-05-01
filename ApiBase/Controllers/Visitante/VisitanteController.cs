using ApiBase.Contracts;
using ApiBase.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.Visitante
{
    [Route("")]
    [ApiController]
    public class VisitanteController(IVisitanteRepository repository) : ControllerBase
    {
        private readonly IVisitanteRepository _repository = repository;

        // Retorna todos os usuários
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> Index()
        {
            IEnumerable<UsuarioDto> usuarios = await _repository.Index();

            if (!usuarios.Any())
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<VisitanteDto>> Get(int userId)
        {
            VisitanteDto visitanteRetorno = await _repository.Get(userId);

            if (visitanteRetorno == null)
            {
                return NotFound();
            }

            return await _repository.Get(userId);
        }
    }
}
