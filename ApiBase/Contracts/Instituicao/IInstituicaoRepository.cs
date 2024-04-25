using ApiBase.Models;

namespace ApiBase.Contracts.Instituicao
{
    public interface IInstituicaoRepository
    {
        Task<bool> Put(InstituicaoModel instituicao, int id);
        Task<bool> Delete(int id);
    }
}
