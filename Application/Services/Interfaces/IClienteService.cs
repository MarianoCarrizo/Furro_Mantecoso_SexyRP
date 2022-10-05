using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IClienteService
    {
        Cliente GetClienteById(int id);

        Cliente GetClienteByDNI(string DNI);

        Cliente CreateClient(string DNI, string nombre, string apellido, string direccion, string telefono);

        List<Cliente> ShowClientes();
    }
}
