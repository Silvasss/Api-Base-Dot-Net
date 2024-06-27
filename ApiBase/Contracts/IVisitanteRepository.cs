using ApiBase.Models;
using ApiBase.Pagination;

namespace ApiBase.Contracts
{
    public interface IVisitanteRepository
    {
        Task<PagedList<Usuario>> Index(VisitanteParameters visitanteParams);
        Task<object> Get(int userId);
    }
}
