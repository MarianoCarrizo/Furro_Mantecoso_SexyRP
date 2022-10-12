using Domain.Entities;
using Domain.Models;

namespace Application.Services.Interfaces
{
     public interface IClienteService
     {
          Task<ClienteDto> GetClienteById(int id);

          Task<ClienteDto> GetClienteByDNI(string DNI);

          Task<ClienteDto> CreateClient(ClienteDto cliente);

          Task<List<ClienteDto>> ShowClientes();
     }
}
