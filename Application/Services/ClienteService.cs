using Application.DataAccess;

using Application.Services.Interfaces;
using Domain.Entities;


namespace Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;


        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public Cliente GetClienteById(int id)
        {
            return _repository.GetClienteById(id);
        }

        public Cliente CreateClient(string dni, string nombre, string apellido, string direccion, string telefono)
        {

            try

            {
                var ClientExists = _repository.GetClienteByDNI(dni);
                if (ClientExists != null)
                {
                    return null;
                }

                var client = new Cliente()
                {
                    Dni = dni,
                    Nombre = nombre,
                    Apellido = apellido,
                    Direccion = direccion,
                    Telefono = telefono,
                };

                _repository.AddCliente(client);
                return client;
            }
            catch (Exception)
            {
                return null;
            }

        }


        public List<Cliente> ShowClientes()
        {


            List<Cliente> list = _repository.GetAllClientes();
            return list;

        }

        public Cliente GetClienteByDNI(string DNI)
        {
            return _repository.GetClienteByDNI(DNI);
        }
    }
}
