using ApiBase.Dtos;
using ApiBase.Pagination;

namespace ApiBase.Contracts.Instituicao
{
    public interface ISoliciticacoesRepository
    {
        Task<object> Dashboard(int id);
        Task<object> Get(int id, int status);
        Task<object> GetSolicitacao(int id);
        Task<bool> Put(SolicitacaoDto solicitaCurso, int userId);
    }
}
