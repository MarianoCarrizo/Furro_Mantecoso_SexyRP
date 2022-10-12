using Application.DataAccess;

using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Models;


namespace Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
          private readonly IMapper _mapper;


          public ClienteService(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
               _mapper = mapper;   
        }

        public async Task<ClienteDto> GetClienteById(int id)
        {
            var cliente = await _repository.GetClienteById(id);
               return _mapper.Map<ClienteDto>(cliente);
          }

        public async Task<ClienteDto> CreateClient(ClienteDto cliente)
        {

            try

            {
                var ClientExists = _repository.GetClienteByDNI(cliente.Dni);
                if (ClientExists.Result != null)
                {
                         return null;
                }

                var client = new Cliente()
                {
                    Dni = cliente.Dni,
                    Nombre = cliente.name,
                    Apellido = cliente.lastname,
                    Direccion = cliente.address,
                    Telefono = cliente.phoneNumber,
                };

               var clientcreado =  await _repository.AddCliente(client);
                    return _mapper.Map<ClienteDto>(clientcreado);
               }
            catch (Exception)
            {
                return null;
            }

        }


        public async Task<List<ClienteDto>> ShowClientes()
        {


            var  list = await _repository.GetAllClientes();
               return _mapper.Map<List<ClienteDto>>(list);

          }

        public async Task<ClienteDto> GetClienteByDNI(string DNI)
        {
               var cliente = await _repository.GetClienteByDNI(DNI);
               return _mapper.Map<ClienteDto>(cliente);
          }
    }
}
