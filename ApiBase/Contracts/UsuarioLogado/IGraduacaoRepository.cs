using ApiBase.Dtos;

namespace ApiBase.Contracts.UsuarioLogado
{
    public interface IGraduacaoRepository
    {
        Task<ListaInfos> Get(int userId);
        Task<bool> Post(GraduacaoDto graduacao, int id);
        Task<bool> Put(GraduacaoDto graduacao, int id);
        Task<bool> Delete(int id, int userId);
    }
}
