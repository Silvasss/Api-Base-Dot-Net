using ApiBase.Dtos;

namespace ApiBase.Contracts.Admin
{
    public interface IAdminRepository
    {
        Task<object> Get();
        Task<IEnumerable<object>> GetAllLogs();
        Task<IEnumerable<object>> GetAllInstituicao();
        Task<object> GetInfoInstituicao(int id);
        Task<IEnumerable<object>> GetAllUsuarios(int userId);
        Task<object> GetUsuario(int id);
        Task<bool> Post(InstituicaoDtoCreate instituicaoInsert);
        Task<bool> Delete(int userId);
        Task<bool> DeleteUsuario(int userId);
        Task<bool> Put(int userId, int roleId);
        Task<bool> GerarPopulacao(int quantidade);
    }
}
