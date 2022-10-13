using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface ICarritoService
    {
        Carrito GetCarritoByClientId(int id);

        Carrito GetCarritoById(Guid id);

       Task<Carrito> CreateCarrito(Carrito carrito);

        CarritoProducto DeleteCarritoProducto(CarritoProducto carrito);

          Task<CarritoProducto> UpdateCarritoProducto(CarritoProducto carrito);

          CarritoProducto GetCarritoProductoById(Guid id, int productoId);

        CarritoProducto CreateCarritoProducto(CarritoProducto carritoProducto);

         Task<Carrito> UpdateCarrito(Carrito carrito);






    }
}
