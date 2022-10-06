using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface ICarritoService
    {
        Carrito GetCarritoByClientId(int id);

        Carrito GetCarritoById(Guid id);

        Carrito CreateCarrito(Carrito carrito);

        CarritoProducto DeleteCarritoProducto(CarritoProducto carrito);

        CarritoProducto GetCarritoProductoById(Guid id, int productoId);

        CarritoProducto CreateCarritoProducto(CarritoProducto carritoProducto);

        Carrito UpdateCarrito(Carrito carrito);






    }
}
