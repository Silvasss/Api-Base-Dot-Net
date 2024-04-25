using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Contracts.Usuario
{
    public interface IUsuarioRepository
    {
        Task<ActionResult<UserCompleto>> Get(int id);
        Task<bool> Put(User user);
        Task<bool> Post(UserForLoginDto userForSetPassword);
        Task<bool> Delete(int id);
    }
}
