using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDto> GetClienteById(int id);

        Task<ClienteDto> GetClienteByEmailAndPasword(String Email, String Password);

        Task<ClienteDto> CreateClient(ClienteDto cliente);

    }
}
