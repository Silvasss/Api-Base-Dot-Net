using ApiBase.Dtos;

namespace ApiBase.Contracts.UsuarioLogado
{
    public interface IExperienciaRepository
    {
        Task<bool> Post(ExperienciaDto experiencia, int id);
        Task<bool> Put(ExperienciaDto experiencia, int id);
        Task<bool> Delete(int id, int Experienciaid);
    }
}
