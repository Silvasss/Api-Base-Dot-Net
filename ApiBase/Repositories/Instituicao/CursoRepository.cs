using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class CursoRepository(IConfiguration config) : ICursoRepository
    {
        private readonly DataContextEF _entityFramework = new(config);
        private readonly Mapper _mapper = new(new MapperConfiguration(cfg => { cfg.CreateMap<Curso, CursoDto>().ReverseMap(); }));

        public async Task<bool> Delete(int cursoId, int id)
        {
            Curso? cursoDb = await _entityFramework.Curso.Where(c => c.Curso_Id == cursoId && c.Instituicao_Id == id).FirstAsync();

            if (cursoDb != null)
            {
                _entityFramework.Remove(cursoDb);

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<IEnumerable<CursoDto>> Get(int id)
        {
            return _mapper.Map<IEnumerable<CursoDto>>(await _entityFramework.Curso.Where(c => c.Instituicao_Id == id).ToListAsync());
        }

        public async Task<bool> Post(CursoDto curso, int id)
        {
            Curso cursoDb = new()
            {
                Instituicao_Id = id,
                Nome = curso.Nome
            };

            await _entityFramework.AddAsync(cursoDb);

            if (await _entityFramework.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Put(CursoDto curso, int id)
        {
            Curso? cursoDb = await _entityFramework.Curso.Where(c => c.Curso_Id == curso.Curso_Id && c.Instituicao_Id == id).FirstAsync();

            if (cursoDb != null)
            {
                cursoDb.Nome = curso.Nome;
                cursoDb.Ativo = curso.Ativo;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
