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

          public async Task<List<Cliente>> GetClientes(string? nombre = null, string? apellido = null, string? dni = null)
          {
               return _context.Clientes.
                                       Where(Client => (string.IsNullOrEmpty(nombre) || Client.Nombre == nombre) &&
                                       (string.IsNullOrEmpty(apellido) || Client.Apellido == apellido) &&
                                       (string.IsNullOrEmpty(dni) || Client.Dni == dni)).ToList();
          }

          public Task<List<Cliente>> GetAllClientes()
          {
               var clientes = _context.Clientes.ToList();
               return Task.FromResult(clientes);
          }

          public void UpdateCliente(Cliente cliente)
          {
               _context.Clientes.Update(cliente);
               _context.SaveChanges();
          }

          public async Task<Cliente> GetClienteByDNI(string DNI)
          {
               return _context.Clientes.FirstOrDefault(client => client.Dni == DNI);

          }


     }
}
