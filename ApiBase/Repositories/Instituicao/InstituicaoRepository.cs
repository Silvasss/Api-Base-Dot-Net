using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class InstituicaoRepository(IConfiguration config) : IInstituicaoRepository
    {
        private readonly DataContextEF _entityFramework = new(config);

        public async Task<bool> Delete(int id)
        {
            InstituicaoEF? instituicaoDb = await _entityFramework.Instituicao.Where(i => i.Instituicao_Id == id).FirstAsync();

            if (instituicaoDb != null)
            {
                instituicaoDb.Ativo = false;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Put(InstituicaoInsert instituicao, int id)
        {
            InstituicaoEF? instituicaoDb = await _entityFramework.Instituicao.Where(i => i.Instituicao_Id == id).FirstAsync();

            if (instituicaoDb != null)
            {
                instituicaoDb.Nome = instituicao.Nome;
                instituicaoDb.PlusCode = instituicao.PlusCode;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}