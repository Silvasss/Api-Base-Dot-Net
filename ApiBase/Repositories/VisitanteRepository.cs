using ApiBase.Contracts;
using ApiBase.Data;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBase.Repositories
{
    public class VisitanteRepository(IConfiguration config) : IVisitanteRepository
    {
        private readonly DataContextEF _entityFramework = new(config);

        public async Task<Usuario> All(int userId)
        {
            return await _entityFramework.Usuarios.Where(u => u.Usuario_Id == userId && u.TipoConta.Nome == "usuario").FirstAsync();
        }

        public async Task<IEnumerable<Usuario>> Index()
        {
            return await _entityFramework.Usuarios.Where(u => u.TipoConta.Nome == "usuario").ToListAsync();
        }
    }
}
