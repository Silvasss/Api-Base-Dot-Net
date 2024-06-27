using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBase.Repositories.UsuarioLogado
{
    public class GraduacaoRepository(DataContextEF dataContext) : IGraduacaoRepository
    {
        private readonly DataContextEF _entityFramework = dataContext;

        public async Task<object> Get(int userId)
        {
            return new Dictionary<string, object>
            {
                {
                    "graduacoes", await _entityFramework.Graduacaos
                    .AsNoTracking()
                    .Where(u => u.Usuario_Id == userId)
                    .Select(u => new
                        {
                            u.Graduacao_Id,
                            Situacao = u.Situacao.ToString(),
                            u.Tipo,
                            u.Inicio,
                            u.Fim,
                            u.Curso_Id,
                            u.CursoNome,
                            u.InstituicaoId,
                            u.InstituicaoNome
                        }
                    ).ToListAsync()
                },
                {
                    "listaInstituicoes", await _entityFramework.Instituicao
                    .AsNoTracking()
                    .Select(i => new 
                        {
                            i.Nome,
                            i.Instituicao_Id,
                            Cursos = i.Cursos.Where(c => c.Ativo == true)
                                .Select(c => new
                                {
                                    c.Curso_Id,
                                    c.Nome,
                                    c.Ativo
                                })
                        }
                    ).ToListAsync()
                }
            };
        }

        public async Task<bool> Delete(int id, int userId)
        {
            Graduacao? graduacaoDb = await _entityFramework.Graduacaos.Where(g => g.Graduacao_Id == id && g.Usuario_Id == userId).FirstOrDefaultAsync();

            if (graduacaoDb != null)
            {
                _entityFramework.Remove(graduacaoDb);

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Post(GraduacaoDto graduacao, int id)
        {
            Graduacao graduacaoDb = new()
            {
                Situacao = (SituacaoGraduacao)Enum.Parse(typeof(SituacaoGraduacao), graduacao.Situacao),
                Tipo = graduacao.Tipo,
                Inicio = graduacao.Inicio,
                Fim = graduacao.Fim,
                Usuario_Id = id,
                Curso_Id = graduacao.Curso_Id,
                CursoNome = graduacao.CursoNome,
                InstituicaoId = graduacao.InstituicaoId,
                InstituicaoNome = graduacao.InstituicaoNome
            };

            Solicitacao solicitacaoModel = new()
            {
                Instituicao_Id = graduacao.InstituicaoId
            };

            graduacaoDb.Solicitacao = solicitacaoModel;

            await _entityFramework.AddAsync(graduacaoDb);

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Put(GraduacaoDto graduacao, int id)
        {
            Graduacao? graduacaoDb = await _entityFramework.Graduacaos.Where(g => g.Graduacao_Id == graduacao.Graduacao_Id && g.Usuario_Id == id).FirstOrDefaultAsync();

            if (graduacaoDb != null)
            {
                if (!String.IsNullOrEmpty(graduacao.ConteudoReposta))
                {
                    RespostaSolicitacao local = new()
                    {
                        ConteudoReposta = graduacao.ConteudoReposta,
                        Origem = OrigemResposta.Usuario,
                        Solicitacao_Id = graduacao.Solicitacao_Id
                    };

                    await _entityFramework.AddAsync(local);
                }

                graduacaoDb.Situacao = (SituacaoGraduacao)Enum.Parse(typeof(SituacaoGraduacao), graduacao.Situacao);

                await _entityFramework.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<object> GetSolicitacao(int id)
        {
            return await _entityFramework.Solicitacao
                .AsNoTracking()
                .Where(s => s.Graduacao_Id == id)
                .Include(s => s.Respostas)
                .Select(r => new
                {
                    r.Solicitacao_Id,
                    status = r.Status.ToString(),
                    r.CreatedAt,
                    resposta = r.Respostas
                        .Select(n => new
                        {
                            n.Resposta_Id,
                            n.ConteudoReposta,
                            n.Origem,
                            n.CreatedAt
                        })
                })
                .FirstAsync();
        }
    }
}