using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class InstituicaoRepository(IConfiguration config) : IInstituicaoRepository
    {
        private readonly DataContextEF _entityFramework = new(config);
        private readonly Mapper _mapper = new(new MapperConfiguration(cfg => { cfg.CreateMap<InstituicaoEF, InstituicaoDto>().ReverseMap(); }));

        public async Task<InstituicaoDto> Get(int userId)
        {
            return _mapper.Map<InstituicaoDto>(await _entityFramework.Instituicao.Where(i => i.Instituicao_Id == userId).FirstAsync());
        }

        public async Task<bool> Delete(int id)
        {
            InstituicaoEF instituicaoDb = await _entityFramework.Instituicao.Where(i => i.Instituicao_Id == id).FirstAsync();

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

        public async Task<bool> Put(InstituicaoDto instituicao, int id)
        {
            InstituicaoEF instituicaoDb = await _entityFramework.Instituicao.Where(i => i.Instituicao_Id == id).FirstAsync();

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