using ApiBase.Dtos;
using ApiBase.Models;

namespace ApiBase.Contracts.Admin
{
    public interface IAdminRepository
    {
        Task<AdminDashboard> Get();
        Task<IEnumerable<SerilogEntry>> GetAllLogs();
        Task<IEnumerable<InstituicaoEF>> GetAllInstituicao();
        Task<IEnumerable<Usuario>> GetAllUsuarios();
        Task<bool> Post(InstituicaoDtoCreate instituicaoInsert);
        Task<bool> Delete(int userId);
        Task<bool> Put(int userId, int roleId);
    }
}
