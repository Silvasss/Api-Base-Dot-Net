using ApiBase.Models;

namespace ApiBase.Contracts.Usuario
{
    public interface IGraduacaoRepository
    {
        Task<bool> Post(Graduacao graduacao, int id);
        Task<bool> Put(Graduacao graduacao, int id);
        Task<bool> Delete(int id);
    }
}
