using ApiBase.Contracts;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiBase.Repositories
{
    public class VisitanteRepository(IConfiguration config) : IVisitanteRepository
    {
        private readonly DataContextEF _entityFramework = new(config);
        private readonly Mapper _mapper = new(new MapperConfiguration(cfg => { cfg.CreateMap<Curso, CursoDto>().ReverseMap(); }));

        public async Task<UsuarioDto> All(int userId)
        {
            return _mapper.Map<UsuarioDto>(await _entityFramework.Usuarios.Where(u => u.Usuario_Id == userId && u.TipoConta.Nome == "usuario").FirstAsync());
        }

        public async Task<IEnumerable<UsuarioDto>> Index()
        {
            return (IEnumerable<UsuarioDto>)_mapper.Map<UsuarioDto>(await _entityFramework.Usuarios.Where(u => u.TipoConta.Nome == "usuario").ToListAsync());
        }
    }
}
