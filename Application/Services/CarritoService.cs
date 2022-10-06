using Application.DataAccess;
using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _CarritoRepository;

        public CarritoService(ICarritoRepository repoC)
        {
            _CarritoRepository = repoC;
        }

        public Carrito GetCarritoByClientId(int id)
        {
            return _CarritoRepository.GetCarritoByClientId(id);
        }

        public Carrito GetCarritoById(Guid id)
        {
            return _CarritoRepository.GetCarritoById(id);
        }

        public void MostrarCarrito(Guid cliente)
        {
            Console.Clear();
            var carritoContent = _CarritoRepository.GetCarritoById(cliente);
            if (carritoContent.CarritoProductos.Count != 0)
            {
                Console.WriteLine("MOSTRANDO CARRITO");
                int count = 1;
                foreach (var pr in carritoContent.CarritoProductos)
                {
                    Console.WriteLine(
                        "{0} ) || producto: {1} || precio: {2} || marca: {3} || descripcion: {4}",
                        count,
                        pr.Producto.Nombre,
                        pr.Producto.Precio,
                        pr.Producto.Marca,
                        pr.Producto.Descripcion
                    );
                    Console.WriteLine("");
                    count++;
                }
            }
            else
            {
                Console.WriteLine("el carrito está vacio... ");
            }
        }



        public Carrito UpdateCarrito(Carrito carrito)
        {
            return _CarritoRepository.UpdateCarrito(carrito);
        }

        public CarritoProducto DeleteCarritoProducto(CarritoProducto carrito)
        {
            return _CarritoRepository.DeleteCarritoProducto(carrito);
        }

        public CarritoProducto GetCarritoProductoById(Guid id, int productoId)
        {
            return _CarritoRepository.GetCarritoProductoById(id, productoId);
        }

        public Carrito CreateCarrito(Carrito carrito)
        {
            return _CarritoRepository.CreateCarrito(carrito);
        }

        public CarritoProducto CreateCarritoProducto(CarritoProducto carritoProducto)
        {
            return _CarritoRepository.CreateCarritoProducto(carritoProducto);
        }
    }



}
