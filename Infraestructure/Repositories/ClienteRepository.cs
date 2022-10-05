using Application.DataAccess;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public Cliente AddCliente(Cliente cliente)
        {
            _context.Add(cliente);
            _context.SaveChanges();

            return cliente;
        }

        public Cliente GetClienteById(int id)
        {

            return _context.Clientes.Find(id);
        }

        public List<Cliente> GetClientes(string? nombre = null, string? apellido = null, string? dni = null)
        {
            return _context.Clientes.
                                    Where(Client => (string.IsNullOrEmpty(nombre) || Client.Nombre == nombre) &&
                                    (string.IsNullOrEmpty(apellido) || Client.Apellido == apellido) &&
                                    (string.IsNullOrEmpty(dni) || Client.Dni == dni)).ToList();
        }

        public List<Cliente> GetAllClientes()
        {
            return _context.Clientes.ToList();
        }

        public void UpdateCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }

        public Cliente GetClienteByDNI(string DNI)
        {
            return _context.Clientes.FirstOrDefault(client => client.Dni == DNI);

        }


    }
}
