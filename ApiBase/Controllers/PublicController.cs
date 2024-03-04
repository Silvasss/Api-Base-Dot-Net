using ApiBase.Data;
using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;


namespace ApiBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublicController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);

        // Retorna todos os perfil do banco
        [HttpGet("getAll")]
        public IEnumerable<UserComplete> GetAll()
        {
            string sql = @"EXEC DotNetDatabase.spUsuario_Get";

            return _dapper.LoadData<UserComplete>(sql);
        }

        // Informações do perfil
        [HttpGet("perfil")]
        public IEnumerable<UserComplete> GetPerfil(string userId)
        {
            string userIdSql = @"EXEC DotNetDatabase.spUsuario_Get @UserId='" +
                userId + "'";

            return _dapper.LoadData<UserComplete>(userIdSql);
        }

        // Informações das medicaçõe
        [HttpGet("experiencia")]
        public IEnumerable<ExperienciaCompleto> GetExperiencia(string userId)
        {
            string userIdSql = @"EXEC DotNetDatabase.spExperiencia_Get @UserId='" +
                userId + "'";

            return _dapper.LoadData<ExperienciaCompleto>(userIdSql);
        }

        // Informações das graduações
        [HttpGet("graduacoes")]
        public IEnumerable<GraduacaoCompleto> GetGraduacoes(string userId)
        {
            string userIdSql = @"EXEC DotNetDatabase.spGraduacao_Get @UserId='" +
                userId + "'";

            return _dapper.LoadData<GraduacaoCompleto>(userIdSql);
        }

    }
}
