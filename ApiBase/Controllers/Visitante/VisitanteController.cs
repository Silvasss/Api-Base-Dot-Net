using ApiBase.Contracts;
using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.Visitante
{
    [Route("api/v1")]
    [ApiController]
    public class VisitanteController(IVisitanteRepository repository) : ControllerBase
    {
        private readonly IVisitanteRepository _repository = repository;

        // Retorna todos os usuários
        [HttpGet]
        public Task<IEnumerable<User>> Index()
        {
            return _repository.Index();
        }

        [HttpGet("{userId}")]
        public async Task<UserCompleto> All(int userId)
        {
            return await _repository.All(userId);
        }
    }
}
