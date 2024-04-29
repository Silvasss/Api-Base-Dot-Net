using ApiBase.Models;

namespace ApiBase.Contracts
{
    public interface IVisitanteRepository
    {
        Task<IEnumerable<Usuario>> Index();
        Task<Usuario> All(int userId);
    }
}
