using ApiBase.Dtos;

namespace ApiBase.Contracts.Instituicao
{
    public interface ICursoRepository
    {
        Task<IEnumerable<CursoDto>> Get(int id);
        Task<bool> Post(CursoDto curso, int id);
        Task<bool> Put(CursoDto curso, int id);
        Task<bool> Delete(int cursoId, int id);
    }
}
