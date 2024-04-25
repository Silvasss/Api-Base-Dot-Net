using ApiBase.Models;

namespace ApiBase.Contracts.Instituicao
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Curso>> Get(int id);
        Task<bool> Post(Curso curso, int id);
        Task<bool> Put(Curso curso, int id);
        Task<bool> Delete(int cursoId, int id);
    }
}
