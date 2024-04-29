using ApiBase.Dtos;
using ApiBase.Models;

namespace ApiBase.Contracts.Instituicao
{
    public interface ISoliciticacoesRepository
    {
        Task<IEnumerable<SolicitacaoDto>> Get(int id);
        Task<SolicitacaoDto> GetSolicitacao(int id, int InstituicaoId);
        Task<bool> Put(SolicitacaoDto solicitaCurso, int id);
    }
}
