using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class SoliciticacoesRepository(DataContextEF dataContext) : ISoliciticacoesRepository
    {
        private readonly DataContextEF _entityFramework = dataContext;

        public async Task<object> Dashboard(int id)
        {
            int totalPendentes = await _entityFramework.Solicitacao.AsNoTracking().Where(s => s.Instituicao_Id == id).CountAsync(s => s.Status == Status.Pendente);

            int totalRecusadas = await _entityFramework.Solicitacao.AsNoTracking().Where(s => s.Instituicao_Id == id).CountAsync(s => s.Status == Status.Recusado);

            int totalAceitas = await _entityFramework.Solicitacao.AsNoTracking().Where(s => s.Instituicao_Id == id).CountAsync(s => s.Status == Status.Aceito);

            int totalSolicitacoes = await _entityFramework.Solicitacao.AsNoTracking().Where(s => s.Instituicao_Id == id).CountAsync();

            // Calcular o total
            int total = totalAceitas + totalPendentes + totalRecusadas;

            // Calcular as porcentagens e arredondar para duas casas decimais
            double porcentagemAceitas = Math.Round((double)totalAceitas / total * 100, 2);
            double porcentagemPendentes = Math.Round((double)totalPendentes / total * 100, 2);
            double porcentagemRecusadas = Math.Round((double)totalRecusadas / total * 100, 2);

            return new Dictionary<string, object>
            {
                { "totalPendentes", totalPendentes },
                { "totalRecusadas", totalRecusadas },
                { "totalAceitas", totalAceitas },
                { "totalSolicitacoes", totalSolicitacoes },
                { "porcentagemAceitas", Double.IsNaN(porcentagemAceitas) ? 0 : porcentagemAceitas },
                { "porcentagemPendentes", Double.IsNaN(porcentagemPendentes) ? 0 : porcentagemPendentes },
                { "porcentagemRecusadas", Double.IsNaN(porcentagemRecusadas) ? 0 : porcentagemRecusadas }
            };
        }

        public async Task<object> Get(int id, int status)
        {
            // Verificar se o valor 'status' 
            var temp = (Status)Enum.Parse(typeof(Status), Enum.GetName(typeof(Status), status));

            return new Dictionary<string, object>
            {
                { "pendentes", await _entityFramework.Solicitacao.AsNoTracking().Where(s => s.Instituicao_Id == id).CountAsync(s => s.Status == Status.Pendente) },
                { "recusadas", await _entityFramework.Solicitacao.AsNoTracking().Where(s => s.Instituicao_Id == id).CountAsync(s => s.Status == Status.Recusado) },
                {
                    "solicitacoes", await _entityFramework.Solicitacao
                        .AsNoTracking()
                        .Where(s => s.Instituicao_Id == id && s.Status == temp)
                        .Select(s => new
                        {
                            s.Solicitacao_Id,
                            Status = s.Status.ToString(),
                            CreatedAt = s.UpdatedAt
                        })
                        .ToListAsync()}
            };
        }

        public async Task<object> GetSolicitacao(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.

            var solicitacaoDb = await _entityFramework.Solicitacao
                .AsNoTracking()
                .Where(s => s.Solicitacao_Id == id)
                .Include(s => s.Graduacao)
                .Include(s => s.Respostas)
                .Select(n => new
                {
                    n.Solicitacao_Id,
                    n.Status,
                    n.CreatedAt,
                    Respostas = n.Respostas.ToArray(),
                    n.Graduacao.Usuario_Id
                })
                .FirstOrDefaultAsync();

            var visitanteDb = await _entityFramework.Usuarios
                .AsNoTracking()
                .Where(u => u.Usuario_Id == solicitacaoDb.Usuario_Id)
                .Include(u => u.Experiencias)
                .Include(u => u.graduacoes)
                .Select(r => new
                {
                    r.Nome,
                    r.PlusCode,
                    r.Pais,
                    r.Usuario_Id,
                    Experiencias = r.Experiencias.Select(e => new
                    {
                        e.Funcao,
                        e.Inicio,
                        e.Fim,
                        e.Responsabilidade
                    }),
                    Graduacoes = r.graduacoes.Select(g => new
                    {
                        g.Inicio,
                        g.Fim,
                        g.InstituicaoNome,
                        g.Tipo
                    })
                })
                .AsSplitQuery()
                .FirstAsync();

            return new
            {
                solicitacaoDb.Solicitacao_Id,
                Status = solicitacaoDb.Status.ToString(),
                solicitacaoDb.CreatedAt,
                solicitacaoDb.Respostas,
                Visitante = visitanteDb
            };

#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<bool> Put(SolicitacaoDto solicitacao, int userId)
        {
            Solicitacao? solicitacaoDb = await _entityFramework.Solicitacao.Where(s => s.Solicitacao_Id == solicitacao.Solicitacao_Id && s.Instituicao_Id == userId).FirstOrDefaultAsync();

            if (solicitacaoDb != null)
            {
                if (!String.IsNullOrEmpty(solicitacao.Respostas.First().ConteudoReposta))
                {
                    RespostaSolicitacao local = new()
                    {
                        ConteudoReposta = solicitacao.Respostas.First().ConteudoReposta,
                        Origem = OrigemResposta.Instituicao,
                        Solicitacao_Id = solicitacao.Solicitacao_Id
                    };

                    await _entityFramework.AddAsync(local);
                }

                solicitacaoDb.Status = (Status)Enum.Parse(typeof(Status), solicitacao.Status);

                await _entityFramework.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}