using ApiBase.Models;

namespace ApiBase.Contracts.Instituicao
{
    public interface ISoliciticacoesRepository
    {
        Task<IEnumerable<SolicitaCurso>> Get(int id);
        Task<SolicitaCurso> GetSolicitacao(int id, int InstituicaoId);
        Task<bool> Put(SolicitaCurso solicitaCurso, int id);
    }
}
