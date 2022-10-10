using Domain.Entities;
using Domain.Models;

namespace Application.Services.Interfaces
{
     public interface IClienteService
     {
          Task<ClienteDto> GetClienteById(int id);

          Task<ClienteDto> GetClienteByDNI(string DNI);

          Task<ClienteDto> CreateClient(string DNI, string nombre, string apellido, string direccion, string telefono);

          Task<List<ClienteDto>> ShowClientes();
     }
}
