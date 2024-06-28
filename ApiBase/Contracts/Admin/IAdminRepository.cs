using ApiBase.Dtos;
using ApiBase.Pagination;

namespace ApiBase.Contracts.Admin
{
    public interface IAdminRepository
    {
        Task<object> Get();
        Task<PagedList<object>> GetAllLogs(VisitanteParameters visitanteParams);
        Task<PagedList<object>> GetAllInstituicao(VisitanteParameters visitanteParams);
        Task<object> GetInfoInstituicao(int id);
        Task<PagedList<object>> GetAllUsuarios(VisitanteParameters visitanteParams, int userId);
        Task<object> GetUsuario(int id);
        Task<bool> Post(InstituicaoDtoCreate instituicaoInsert);
        Task<bool> Delete(int userId);
        Task<bool> DeleteUsuario(int userId);
        Task<bool> Put(int userId, int roleId);
        Task<bool> GerarPopulacao(int quantidade);
    }
}
