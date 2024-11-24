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
            _CarritoRepository = repoC ?? throw new NullReferenceException(nameof(repoC));
        }

        public async Task<Carrito?> GetCarritoByClientId(int id)
        {
            return await _CarritoRepository.AddAsync(id);
        }

        public async Task<Carrito?> GetCarritoById(Guid id)
        {
            return await _CarritoRepository.CreateCarrito(id);
        }

        public async Task<Carrito> UpdateCarrito(Carrito carrito)
        {
            return await _CarritoRepository.AddAsync(carrito);
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
            throw new FileNotFoundException("CarritoId : " + carritoId + " no encontrado");


        }
    }



}
