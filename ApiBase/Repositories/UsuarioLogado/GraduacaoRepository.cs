using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Data;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.UsuarioLogado
{
    public class GraduacaoRepository(IConfiguration config) : IGraduacaoRepository
    {
        private readonly DataContextEF _entityFramework = new(config);

        public async Task<bool> Delete(int id, int userId)
        {
            Graduacao? graduacaoDb = await _entityFramework.Graduacaos.Where(g => g.Graduacao_Id == id && g.Usuario_Id == userId).FirstAsync();

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

        public async Task<bool> Post(Graduacao graduacao, int id)
        {
            Graduacao graduacaoDb = new()
            {
                Situacao = graduacao.Situacao,
                Inicio = graduacao.Inicio,
                Fim = graduacao.Fim,
                Usuario_Id = id,
                Curso_Id = graduacao.Curso_Id
            };

            Solicitacao solicitacaoModel = new()
            {
                Instituicao_Id = graduacao.InstituicaoId
            };

            graduacaoDb.Solicitacao = solicitacaoModel;

            await _entityFramework.AddAsync(graduacaoDb);

            if (await _entityFramework.SaveChangesAsync() > 0)
            {
                return true;
            }
            
            return false;
        }

        public async Task<bool> Put(Graduacao graduacao, int id)
        {
            Graduacao? graduacaoDb = await _entityFramework.Graduacaos.Where(g => g.Graduacao_Id == graduacao.Graduacao_Id && g.Usuario_Id == id).FirstAsync();

            if (graduacaoDb != null)
            {
                graduacaoDb.Situacao = graduacao.Situacao;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
