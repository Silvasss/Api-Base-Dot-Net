using ApiBase.Dtos;
using ApiBase.Models;

namespace ApiBase.Contracts.Admin
{
    public interface IAdminRepository
    {
        Task<IEnumerable<AuditLogs>> Get();
        Task<IEnumerable<SerilogDb>> GetSerilog();
        Task<bool> Post(InstituicaoDtoCreate instituicaoInsert);
        Task<bool> Delete(int userId);
        Task<bool> Put(int userId, int roleId);
    }
}
