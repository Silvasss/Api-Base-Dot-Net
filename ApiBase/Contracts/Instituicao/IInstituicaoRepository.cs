using ApiBase.Dtos;
using ApiBase.Models;

namespace ApiBase.Contracts.Instituicao
{
    public interface IInstituicaoRepository
    {
        Task<bool> Put(InstituicaoDto instituicao, int id);
        Task<bool> Delete(int id);
    }
}
