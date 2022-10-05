using Domain.Entities;

namespace Application.DataAccess
{
    public interface IClienteRepository
    {

        public Cliente GetClienteById(int id);

        public void UpdateCliente(Cliente cliente);

        public Cliente AddCliente(Cliente cliente);
        public Cliente GetClienteByDNI(string DNI);

        public List<Cliente> GetAllClientes();
    }
}
