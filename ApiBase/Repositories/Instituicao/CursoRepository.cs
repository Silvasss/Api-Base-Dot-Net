using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class CursoRepository(DataContextEF dataContext) : ICursoRepository
    {
        private readonly DataContextEF _entityFramework = dataContext;

        public async Task<bool> Delete(int cursoId, int id)
        {
            Curso? cursoDb = await _entityFramework.Curso.Where(c => c.Curso_Id == cursoId && c.Instituicao_Id == id).FirstOrDefaultAsync();

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

        public async Task<IEnumerable<object>> Get(int id)
        {
            return await _entityFramework.Curso
                 .AsNoTracking()
                 .Where(c => c.Instituicao_Id == id)
                 .Select(c => new
                 {
                     c.Curso_Id,
                     c.Nome,
                     c.Ativo
                 })
                 .ToListAsync();
        }

        public async Task<bool> Post(CursoDto curso, int id)
        {
            Curso cursoDb = new()
            {
                Instituicao_Id = id,
                Nome = curso.Nome,
                Ativo = curso.Ativo,
            };

            await _entityFramework.AddAsync(cursoDb);

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Put(CursoDto curso, int id)
        {
            Curso? cursoDb = await _entityFramework.Curso.Where(c => c.Curso_Id == curso.Curso_Id && c.Instituicao_Id == id).FirstOrDefaultAsync();

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
