using ApiBase.Dtos;
using ApiBase.Models;

namespace ApiBase.Contracts.Instituicao
{
    public interface IInstituicaoRepository
    {
        Task<InstituicaoDto> Get(int userId);
        Task<bool> Put(InstituicaoDto instituicao, int id);
    }
}
