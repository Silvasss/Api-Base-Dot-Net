using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class SoliciticacoesRepository(IConfiguration config) : ISoliciticacoesRepository
    {
        private readonly DataContextEF _entityFramework = new(config);

        public async Task<IEnumerable<Solicitacao>> Get(int id)
        {
            return await _entityFramework.Solicitacao.Where(s => s.Instituicao_Id == id).ToListAsync();
        }

        public async Task<Solicitacao> GetSolicitacao(int id, int InstituicaoId)
        {
            return await _entityFramework.Solicitacao.Where(s => s.Solicitacao_Id == id && s.Instituicao_Id == InstituicaoId).FirstAsync();
        }

        public async Task<bool> Put(Solicitacao solicitacao, int id)
        {
            Solicitacao? solicitacaoDb = await _entityFramework.Solicitacao.Where(s => s.Solicitacao_Id == solicitacao.Solicitacao_Id && s.Instituicao_Id == id).FirstAsync();

            if (solicitacaoDb != null)
            {
                solicitacaoDb.Descricao = solicitacao.Descricao;
                solicitacaoDb.Ativo = solicitacao.Ativo;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
