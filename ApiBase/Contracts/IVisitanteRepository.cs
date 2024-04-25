using ApiBase.Models;

namespace ApiBase.Contracts
{
    public interface IVisitanteRepository
    {
        Task<IEnumerable<User>> Index();
        Task<UserCompleto> All(int userId);
    }
}
