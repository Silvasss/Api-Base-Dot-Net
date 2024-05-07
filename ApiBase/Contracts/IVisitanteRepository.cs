using ApiBase.Dtos;
using ApiBase.Models;
using ApiBase.Pagination;

namespace ApiBase.Contracts
{
    public interface IVisitanteRepository
    {
        Task<PagedList<Usuario>> Index(VisitanteParameters visitanteParams);
        Task<VisitanteDto> Get(int userId);
    }
}
