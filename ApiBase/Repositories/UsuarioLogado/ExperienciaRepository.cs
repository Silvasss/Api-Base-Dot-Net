using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.UsuarioLogado
{
    public class ExperienciaRepository(IConfiguration config) : IExperienciaRepository
    {
        private readonly DataContextEF _entityFramework = new(config);
        private readonly Mapper _mapper = new(new MapperConfiguration(cfg => { cfg.CreateMap<Experiencia, ExperienciaDto>().ReverseMap(); }));

        public async Task<IEnumerable<ExperienciaDto>> GetAll(int userId)
        {
            return _mapper.Map<IEnumerable<ExperienciaDto>>(await _entityFramework.Experiencia.Where(u => u.Usuario_Id == userId).ToListAsync());
        }

        public async Task<bool> Delete(int id, int Experienciaid)
        {
            Experiencia? experienciaDb = await _entityFramework.Experiencia.Where(e => e.Experiencia_Id == Experienciaid && e.Usuario_Id == id).FirstOrDefaultAsync();

            if (experienciaDb != null)
            {
                _entityFramework.Remove(experienciaDb);

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Post(ExperienciaDto experiencia, int id)
        {
            Experiencia experienciaDb = new()
            {
                Usuario_Id = id,
                Setor = experiencia.Setor,
                Empresa = experiencia.Empresa,
                PlusCode = experiencia.PlusCode,
                Vinculo = experiencia.Vinculo,
                Ativo = experiencia.Ativo,
                Inicio = experiencia.Inicio,
                Fim = experiencia.Fim
            };

            await _entityFramework.AddAsync(experienciaDb);

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Put(ExperienciaDto experiencia, int id)
        {
            Experiencia? experienciaDb = await _entityFramework.Experiencia.Where(e => e.Experiencia_Id == experiencia.Experiencia_Id && e.Usuario_Id == id).FirstOrDefaultAsync();

            if (experienciaDb != null)
            {
                experienciaDb.Setor = experiencia.Setor;
                experienciaDb.PlusCode = experiencia.PlusCode;
                experienciaDb.Vinculo = experiencia.Vinculo;
                experienciaDb.Ativo = experiencia.Ativo;
                experienciaDb.Inicio = experiencia.Inicio;
                experienciaDb.Fim = experiencia.Fim;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}