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

        public async Task<ClienteDto> CreateClient(string dni, string nombre, string apellido, string direccion, string telefono)
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
