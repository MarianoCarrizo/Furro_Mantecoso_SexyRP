using Domain.Entities;

namespace Application.DataAccess
{
    public interface ICarritoRepository
    {

        public Carrito GetCarritoById(Guid id);
        public Carrito GetRawCarritoById(Guid id);

          public Carrito GetCarritoByClientId(int id);

        public Carrito UpdateCarrito(Carrito Carrito);

        public CarritoProducto UpdateCarritoProducto(CarritoProducto carrito);

        public Carrito CreateCarrito(Carrito carrito);

        public CarritoProducto DeleteCarritoProducto(CarritoProducto carritoProducto);

        public CarritoProducto GetCarritoProductoById(Guid id, int productoId);




    }
}
