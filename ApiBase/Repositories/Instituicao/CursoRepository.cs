using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class CursoRepository(IConfiguration config) : ICursoRepository
    {
        private readonly DataContextEF _entityFramework = new(config);

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

        public async Task<IEnumerable<Curso>> Get(int id)
        {
            return await _entityFramework.Curso.Where(c => c.Instituicao_Id == id).ToListAsync();
        }

        public async Task<bool> Post(Curso curso, int id)
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

        public async Task<bool> Put(Curso curso, int id)
        {
            Curso? cursoDb = await _entityFramework.Curso.Where(c => c.Curso_Id == curso.Curso_Id && c.Instituicao_Id == id).FirstAsync();

            if (cursoDb != null)
            {
                cursoDb.Nome = cursoDb.Nome;
                cursoDb.Ativo = cursoDb.Ativo;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
