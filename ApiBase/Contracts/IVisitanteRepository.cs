using ApiBase.Dtos;
using ApiBase.Models;

namespace ApiBase.Contracts
{
    public interface IVisitanteRepository
    {
        Task<IEnumerable<UsuarioDto>> Index();
        Task<UsuarioDto> All(int userId);
    }
}
