using ApiBase.Contracts;
using ApiBase.Dtos;
using ApiBase.Models;
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
        public async Task<IEnumerable<UsuarioDto>> Index()
        {
            return await _repository.Index();
        }

        [HttpGet("{userId}")]
        public async Task<UsuarioDto> All(int userId)
        {
            return await _repository.All(userId);
        }
    }
}
