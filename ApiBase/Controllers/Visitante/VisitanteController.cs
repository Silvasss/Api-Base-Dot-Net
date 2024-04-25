using ApiBase.Data;
using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.Visitante
{
    [Route("api/v1")]
    [ApiController]
    public class VisitanteController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);

        // Retorna todos os usuários
        [HttpGet]
        public Task<IEnumerable<User>> Index()
        {
            return _dapper.LoadDataAsync<User>(@"EXEC spUser_Get");
        }

        [HttpGet("{userId}")]
        public async Task<UserCompleto> All(string userId)
        {
            UserCompleto usuario = await _dapper.LoadDataSingle<UserCompleto>(@"EXEC spUser_Get @UserId='" + userId + "'");

            usuario.Experiencia = (List<Experiencia>?)await _dapper.LoadDataAsync<Experiencia>(@"EXEC spExperiencia_Get @UserId='" + userId + "'");

            usuario.Graduacao = (List<Graduacao>?)await _dapper.LoadDataAsync<Graduacao>(@"EXEC spGraduacao_Get @UserId='" + userId + "'");

            return usuario;
        }
    }
}
