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
        private readonly Mapper _mapper = new(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Usuario, UsuarioDto>().ReverseMap();
            cfg.CreateMap<Usuario, VisitanteDto>().ReverseMap();
            cfg.CreateMap<Experiencia, ExperienciaDto>().ReverseMap();
            cfg.CreateMap<Graduacao, GraduacaoDto>().ReverseMap();
        }));

        public async Task<VisitanteDto> Get(int userId)
        {
            VisitanteDto visitanteDb = _mapper.Map<VisitanteDto>(await _entityFramework.Usuarios.Where(u => u.Usuario_Id == userId && u.Tipo_Conta_Id == 2).FirstOrDefaultAsync());

            if (visitanteDb == null)
            {
                return visitanteDb;
            }

            visitanteDb.Experiencias = _mapper.Map<IEnumerable<ExperienciaDto>>(await _entityFramework.Experiencia.Where(u => u.Usuario_Id == userId).ToListAsync());

            visitanteDb.Graduacoes = _mapper.Map<IEnumerable<GraduacaoDto>>(await _entityFramework.Graduacaos.Where(u => u.Usuario_Id == userId).ToListAsync());
            
            return visitanteDb;
        }

        public async Task<IEnumerable<UsuarioDto>> Index()
        {
            return _mapper.Map<IEnumerable<UsuarioDto>>(await _entityFramework.Usuarios.Where(u => u.Tipo_Conta_Id == 2).ToListAsync());
        }
    }
}
