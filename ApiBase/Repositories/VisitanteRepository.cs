using ApiBase.Contracts;
using ApiBase.Data;
using ApiBase.Models;

namespace ApiBase.Repositories
{
    public class VisitanteRepository(IConfiguration config) : IVisitanteRepository
    {
        private readonly DataContextDapper _dapper = new(config);

        public async Task<UserCompleto> All(int userId)
        {
            UserCompleto usuario = await _dapper.LoadDataSingle<UserCompleto>(@"EXEC spUser_Get @UserId='" + userId + "'");

            usuario.Experiencia = (List<Experiencia>?)await _dapper.LoadDataAsync<Experiencia>(@"EXEC spExperiencia_Get @UserId='" + userId + "'");

            usuario.Graduacao = (List<Graduacao>?)await _dapper.LoadDataAsync<Graduacao>(@"EXEC spGraduacao_Get @UserId='" + userId + "'");

            return usuario;
        }

        public Task<IEnumerable<User>> Index()
        {
            return _dapper.LoadDataAsync<User>(@"EXEC spUser_Get");
        }
    }
}
