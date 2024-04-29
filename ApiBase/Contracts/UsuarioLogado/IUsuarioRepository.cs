using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Contracts.UsuarioLogado
{
    public interface IUsuarioRepository
    {
        Task<ActionResult<Usuario>> Get(int id);
        Task<bool> Put(Usuario user);
        Task<bool> Delete(int id);
    }
}
