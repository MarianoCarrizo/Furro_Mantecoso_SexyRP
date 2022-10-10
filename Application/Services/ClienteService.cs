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

        public async Task<Cliente> GetClienteById(int id)
        {
            var cliente = await _repository.GetClienteById(id);
               return cliente;
        }

        public Task<Cliente> CreateClient(string dni, string nombre, string apellido, string direccion, string telefono)
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

               var clientcreado =  _repository.AddCliente(client);
                    return clientcreado;
            }
            catch (Exception)
            {
                return null;
            }

        }


        public async Task<List<Cliente>> ShowClientes()
        {


            var  list = await _repository.GetAllClientes();
               return list;

        }

        public async Task<Cliente> GetClienteByDNI(string DNI)
        {
               var cliente = await _repository.GetClienteByDNI(DNI);
               return cliente;
        }
    }
}
