using Application.DataAccess;
using Domain.Entities;
using Infraestructure.Persistance;

namespace Infraestructure.Repositories
{
     public class ClienteRepository : IClienteRepository
     {
          private readonly AppDbContext _context;

          public ClienteRepository(AppDbContext context)
          {
               _context = context;
          }

          public async Task<Cliente> AddCliente(Cliente cliente)
          {
               _context.Add(cliente);
               await _context.SaveChangesAsync();

               return cliente;
          }

          public async Task<Cliente> GetClienteById(int id)
          {

               return _context.Clientes.Find(id);
          }
          public async Task<Cliente> GetClienteByDNI(string DNI)
          {
               return _context.Clientes.FirstOrDefault(client => client.Dni == DNI);

          }

        
     }
}
