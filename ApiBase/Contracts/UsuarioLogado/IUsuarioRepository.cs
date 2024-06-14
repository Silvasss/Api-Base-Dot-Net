using ApiBase.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Contracts.UsuarioLogado
{
    public interface IUsuarioRepository
    {
        Task<ActionResult<UsuarioIndexDto>> Get(int id);
        Task<UsuarioDto> GetConfiguracoes(int userId);
        Task<bool> PutConfiguracoes(List<string> temp, int userId);
        Task<bool> Put(UsuarioDto user);
        Task<bool> Delete(int id);
    }
}
