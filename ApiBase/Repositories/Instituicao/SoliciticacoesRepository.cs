using ApiBase.Contracts.Instituicao;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.Instituicao
{
    public class SoliciticacoesRepository(IConfiguration config) : ISoliciticacoesRepository
    {
        private readonly DataContextEF _entityFramework = new(config);
        private readonly Mapper _mapper = new(new MapperConfiguration(cfg => { cfg.CreateMap<Solicitacao, SolicitacaoDto>().ReverseMap(); }));

        public async Task<IEnumerable<SolicitacaoDto>> Get(int id)
        {
            return _mapper.Map<IEnumerable<SolicitacaoDto>>(await _entityFramework.Solicitacao.Where(s => s.Instituicao_Id == id).ToListAsync());
        }

        public async Task<SolicitacaoDto> GetSolicitacao(int id, int InstituicaoId)
        {
            return _mapper.Map<SolicitacaoDto>(await _entityFramework.Solicitacao.Where(s => s.Solicitacao_Id == id && s.Instituicao_Id == InstituicaoId).FirstOrDefaultAsync());
        }

        public async Task<bool> Put(SolicitacaoDto solicitacao, int id)
        {
            Solicitacao? solicitacaoDb = await _entityFramework.Solicitacao.Where(s => s.Solicitacao_Id == solicitacao.Solicitacao_Id && s.Instituicao_Id == id).FirstOrDefaultAsync();

            if (solicitacaoDb != null)
            {
                solicitacaoDb.Descricao = solicitacao.Descricao;
                solicitacaoDb.Ativo = solicitacao.Ativo;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}