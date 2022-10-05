using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface ICarritoService
    {
        Carrito GetCarritoByClientId(int id);

        Carrito UpdateCarrito(Carrito carrito);






    }
}
