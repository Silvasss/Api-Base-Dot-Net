using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.UsuarioLogado
{
    public class UsuarioRepository(IConfiguration config) : IUsuarioRepository
    {
        private readonly DataContextEF _entityFramework = new(config);
        private readonly Mapper _mapper = new(new MapperConfiguration(cfg => { cfg.CreateMap<Usuario, UsuarioDto>().ReverseMap(); }));

        public async Task<bool> Delete(int id)
        {
            Usuario? dados = await _entityFramework.Usuarios.Where(a => a.Usuario_Id == id).FirstAsync();

            Auth? authDb = await _entityFramework.Auth.Where(a => a.Auth_id == dados.Auth_Id).FirstAsync();

            _entityFramework.Auth.Remove(authDb);

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<ActionResult<UsuarioDto>> Get(int id)
        {
            return _mapper.Map<UsuarioDto>(await _entityFramework.Usuarios.Where(u => u.Usuario_Id == id).FirstAsync());
        }

        public async Task<bool> Put(UsuarioDto user)
        {
            Usuario? userDb = await _entityFramework.Usuarios.Where(a => a.Usuario_Id == user.Usuario_Id).FirstAsync();

            userDb.Nome = user.Nome;
            userDb.Pais = user.Pais;
            userDb.PlusCode = user.PlusCode;

            await _entityFramework.SaveChangesAsync();

            return true;
        }
    }
}
