using ApiBase.Dtos;

namespace ApiBase.Contracts.UsuarioLogado
{
    public interface IGraduacaoRepository
    {
        Task<object> Get(int userId);
        Task<object> GetSolicitacao(int id);
        Task<bool> Post(GraduacaoDto graduacao, int id);
        Task<bool> Put(GraduacaoDto graduacao, int id);
        Task<bool> Delete(int id, int userId);
    }
}
