using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Data;
using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.UsuarioLogado
{
    public class UsuarioRepository(IConfiguration config) : IUsuarioRepository
    {
        private readonly DataContextEF _entityFramework = new(config);

        public async Task<bool> Delete(int id)
        {
            Usuario? dados = await _entityFramework.Usuarios.Where(a => a.Usuario_Id == id).FirstAsync();

            Auth? authDb = await _entityFramework.Auth.Where(a => a.Auth_id == dados.Auth_Id).FirstAsync();

            if (authDb != null) 
            { 
                _entityFramework.Auth.Remove(authDb);

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<ActionResult<Usuario>> Get(int id)
        {
            return await _entityFramework.Usuarios.Where(u => u.Usuario_Id == id).FirstAsync();
        }

        public async Task<bool> Put(Usuario user)
        {
            Usuario? userDb = await _entityFramework.Usuarios.Where(a => a.Usuario_Id == user.Usuario_Id).FirstAsync();

            if (userDb != null)
            { 
                userDb.Nome = user.Nome;
                userDb.Pais = user.Pais;
                userDb.PlusCode = user.PlusCode;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
