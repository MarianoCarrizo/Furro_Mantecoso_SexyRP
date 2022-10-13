using Domain.Entities;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IOrdenService
    {

        Orden CreateOrden(Orden order);

        Task<List<OrdenDto>> GetOrder(DateTime? from = null,DateTime? to = null);


     }
}
