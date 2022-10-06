using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IOrdenService
    {
        Orden GetOrdenById(int id);

        Orden CreateOrden(Orden order);


    }
}
