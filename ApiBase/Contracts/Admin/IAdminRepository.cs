using ApiBase.Models;

namespace ApiBase.Contracts.Admin
{
    public interface IAdminRepository
    {
        Task<IEnumerable<AuditLogs>> Get();
        Task<bool> Post(InstituicaoInsert instituicaoInsert);
        Task<bool> Delete(int userId);
        Task<bool> Put(int userId, int roleId);
    }
}
