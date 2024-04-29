using ApiBase.Models;

namespace ApiBase.Contracts.Instituicao
{
    public interface ISoliciticacoesRepository
    {
        Task<IEnumerable<Solicitacao>> Get(int id);
        Task<Solicitacao> GetSolicitacao(int id, int InstituicaoId);
        Task<bool> Put(Solicitacao solicitaCurso, int id);
    }
}
