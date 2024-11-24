using Domain.Entities;

namespace Application.Services.Interfaces
{
    /// <summary>
    /// Document me, daddy <3
    /// </summary>
    public interface ICarritoService
    {
        /// <summary>
        /// To be documented.
        /// </summary>
        /// <param name="id">The id of the client. If you are using more than one type of id, make sure to be specific when naming them.</param>
        /// <returns></returns>
        Task<Carrito?> GetCarritoByClientId(int id);
        Task<Carrito?> GetCarritoById(Guid id);
        Task<Carrito?> GetRawCarritoById(Guid id);
        Task<Carrito> CreateCarrito(Carrito carrito);
        Task<CarritoProducto?> DeleteCarritoProducto(CarritoProducto carrito);
        Task<Carrito?> DeleteCarrito(Guid carritoId);
        Task<CarritoProducto?> UpdateCarritoProducto(CarritoProducto carrito);
        Task<CarritoProducto?> GetCarritoProductoById(Guid id, int productoId);
        Task<Carrito> UpdateCarrito(Carrito carrito);
    }
}
