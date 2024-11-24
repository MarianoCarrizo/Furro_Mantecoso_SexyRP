using Application.DataAccess;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Models;

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

        public async Task<Carrito> UpdateCarrito(Carrito carrito)
        {
            return _CarritoRepository.UpdateCarrito(carrito);
        }

        public async Task<CarritoProducto> DeleteCarritoProducto(CarritoProducto carrito)
        {
            return _CarritoRepository.DeleteCarritoProducto(carrito);
        }

        public CarritoProducto GetCarritoProductoById(Guid id, int productoId)
        {
            return _CarritoRepository.GetCarritoProductoById(id, productoId);
        }

        public async Task<Carrito> CreateCarrito(Carrito carrito)
        {
            return _CarritoRepository.CreateCarrito(carrito);
        }


        public async Task<CarritoProducto> UpdateCarritoProducto(CarritoProducto carrito)
        {

            return _CarritoRepository.UpdateCarritoProducto(carrito);
        }

        public Carrito GetRawCarritoById(Guid id)
        {
            return _CarritoRepository.GetRawCarritoById(id);
        }

        public Task<Carrito> DeleteCarrito(Guid carritoId)
        {
            var carrito = _CarritoRepository.GetRawCarritoById(carritoId);
            if (carrito != null)
            {
                return Task.FromResult(_CarritoRepository.DeleteCarritoById(carritoId));
            }
            throw new FileNotFoundException("CarritoId : "+carritoId+" no encontrado");
            

        }
    }



}
