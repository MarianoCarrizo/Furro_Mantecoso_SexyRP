using Domain.Entities;
using Domain.Models;

namespace Application.Services.Interfaces
{
     public interface IClienteService
     {
          Task<ClienteDto> GetClienteById(int id);


          Task<ClienteDto> CreateClient(ClienteDto cliente);

     }
}
