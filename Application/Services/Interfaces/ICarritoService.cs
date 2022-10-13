using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface ICarritoService
    {
        Carrito GetCarritoByClientId(int id);

        Carrito GetCarritoById(Guid id);
        Carrito GetRawCarritoById(Guid id);

        Task<Carrito> CreateCarrito(Carrito carrito);

        Task<CarritoProducto> DeleteCarritoProducto(CarritoProducto carrito);

        Task<CarritoProducto> UpdateCarritoProducto(CarritoProducto carrito);

        CarritoProducto GetCarritoProductoById(Guid id, int productoId);



        Task<Carrito> UpdateCarrito(Carrito carrito);






    }
}
