using ApiBase.Contracts;
using ApiBase.Data;
using ApiBase.Models;
using ApiBase.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiBase.Repositories
{
    public class VisitanteRepository(DataContextEF dataContext) : IVisitanteRepository
    {
        private readonly DataContextEF _entityFramework = dataContext;

        public async Task<object> Get(int userId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _entityFramework.Usuarios
                .AsNoTracking()
                .Where(u => u.Usuario_Id == userId && u.Tipo_Conta_Id == 2)
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
                    Experiencias = r.Experiencias.Select(e => new
                    {
                        e.Empresa,
                        e.PlusCode,
                        e.Vinculo,
                        e.Funcao,
                        e.Inicio,
                        e.Fim,
                        e.Responsabilidade
                    }),
                    Graduacoes = r.graduacoes.Select(g => new
                    {
                        g.Situacao,
                        g.Inicio,
                        g.Fim,
                        g.InstituicaoNome,
                        g.CursoNome
                    })
                })
                .AsSplitQuery()
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<PagedList<Usuario>> Index(VisitanteParameters visitanteParams)
        {
            var generico = await _entityFramework.Set<Usuario>().Where(u => u.Tipo_Conta_Id == 2).AsNoTracking().ToListAsync();

            var usuarios = generico.OrderBy(v => v.Usuario_Id).AsQueryable();

            var usuariosOrdenados = PagedList<Usuario>.ToPagedList(usuarios, visitanteParams.PageNumber, visitanteParams.PageSize);

            return usuariosOrdenados;
        }
    }
}
