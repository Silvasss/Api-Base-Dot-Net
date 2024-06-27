using ApiBase.Dtos;

namespace ApiBase.Contracts.UsuarioLogado
{
    public interface IUsuarioRepository
    {
        Task<object> Get(int id);
        Task<object> GetConfiguracoes(int userId);
        Task<bool> PutConfiguracoes(List<string> temp, int userId);
        Task<bool> Put(UsuarioDto user);
        Task<bool> Delete(int id);
    }
}
