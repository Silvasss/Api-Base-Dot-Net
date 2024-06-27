using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.UsuarioLogado
{
    public class UsuarioRepository(DataContextEF dataContext) : IUsuarioRepository
    {
        private readonly DataContextEF _entityFramework = dataContext;

        public async Task<bool> Delete(int id)
        {
            Usuario? dados = await _entityFramework.Usuarios.Where(a => a.Usuario_Id == id).FirstAsync();

            Auth? authDb = await _entityFramework.Auth.Where(a => a.Auth_id == dados.Auth_Id).FirstAsync();

            _entityFramework.Auth.Remove(authDb);

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<object> Get(int id)
        {
            return await _entityFramework.Usuarios
                .AsNoTracking()
                .Where(u => u.Usuario_Id == id)
                .Include(u => u.Experiencias)
                .Include(u => u.graduacoes)
                .Select(r => new
                {
                    r.Nome,
                    r.PlusCode,
                    r.Pais,
                    r.SobreMin,
                    r.CargoPrincipal,
                    r.PortfolioURL,
                    r.Email,
                    r.Experiencia,
                    Emprego = r.Experiencias.Select(e => new
                    {
                        e.Funcao,
                        e.Inicio,
                        e.Fim,
                        e.Responsabilidade
                    }),
                    Educacao = r.graduacoes.Select(g => new
                    {
                        g.Inicio,
                        g.Fim,
                        g.InstituicaoNome,
                        g.CursoNome
                    })
                })
                .AsSplitQuery()
                .FirstAsync();
        }

        public async Task<bool> Put(UsuarioDto user)
        {
            Usuario? userDb = await _entityFramework.Usuarios.Where(a => a.Usuario_Id == user.Usuario_Id).FirstAsync();

            userDb.Nome = user.Nome;
            userDb.Pais = user.Pais;
            userDb.PlusCode = user.PlusCode;
            userDb.SobreMin = user.SobreMin;
            userDb.CargoPrincipal = user.CargoPrincipal;
            userDb.Email = user.Email;
            userDb.PortfolioURL = user.PortfolioURL;
            userDb.Experiencia = user.Experiencia;

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PutConfiguracoes(List<string> temp, int userId)
        {
            Usuario? userDb = await _entityFramework.Usuarios.Where(a => a.Usuario_Id == userId).FirstAsync();

            userDb.ConfiguracoesConta = temp;

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<object> GetConfiguracoes(int id)
        {
            return await _entityFramework.Usuarios
                .AsNoTracking()
                .Where(u => u.Usuario_Id == id)
                .Select(u => new
                {
                    u.ConfiguracoesConta
                })
                .FirstAsync();
        }
    }
}
