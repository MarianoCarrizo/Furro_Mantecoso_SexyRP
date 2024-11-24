using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IOrdenService
    {

        Orden CreateOrden(Orden order);

        Task<OrdenResponse> GetOrder(int limit, int page, DateTime? from = null, DateTime? to = null);


    }
}
