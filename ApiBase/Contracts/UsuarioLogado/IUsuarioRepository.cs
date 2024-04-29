using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Contracts.UsuarioLogado
{
    public interface IUsuarioRepository
    {
        Task<ActionResult<UsuarioDto>> Get(int id);
        Task<bool> Put(UsuarioDto user);
        Task<bool> Delete(int id);
    }
}
