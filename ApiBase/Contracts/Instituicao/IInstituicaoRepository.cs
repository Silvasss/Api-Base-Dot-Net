using ApiBase.Dtos;

namespace ApiBase.Contracts.Instituicao
{
    public interface IInstituicaoRepository
    {
        Task<object> Get(int userId);
        Task<bool> Put(InstituicaoDto instituicao, int id);
    }
}
