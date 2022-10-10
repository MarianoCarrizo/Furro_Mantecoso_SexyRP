using Domain.Entities;

namespace Application.Services.Interfaces
{
     public interface IClienteService
     {
          Task<Cliente> GetClienteById(int id);

          Task<Cliente> GetClienteByDNI(string DNI);

          Task<Cliente> CreateClient(string DNI, string nombre, string apellido, string direccion, string telefono);

          Task<List<Cliente>> ShowClientes();
     }
}
