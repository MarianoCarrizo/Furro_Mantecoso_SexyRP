using Domain.Entities;

namespace Application.DataAccess
{
     public interface IClienteRepository
     {

          public Task<Cliente> GetClienteById(int id);
          public Task<Cliente> AddCliente(Cliente cliente);
          public Task<Cliente> GetClienteByDNI(string DNI);

     }
}
