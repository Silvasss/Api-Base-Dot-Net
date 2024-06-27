using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class InstituicaoRepository(DataContextEF dataContext) : IInstituicaoRepository
    {
        private readonly DataContextEF _entityFramework = dataContext;

        public async Task<object> Get(int userId)
        {
            return await _entityFramework.Instituicao
                .AsNoTracking()
                .Where(i => i.Instituicao_Id == userId)
                .Select(i => new
                {
                    i.Nome,
                    i.PlusCode
                })
                .FirstAsync();
        }

        public async Task<bool> Put(InstituicaoDto instituicao, int id)
        {
            InstituicaoEF instituicaoDb = await _entityFramework.Instituicao.Where(i => i.Instituicao_Id == id).FirstAsync();

            instituicaoDb.Nome = instituicao.Nome;
            instituicaoDb.PlusCode = instituicao.PlusCode;

            await _entityFramework.SaveChangesAsync();

            return true;
        }
    }
}