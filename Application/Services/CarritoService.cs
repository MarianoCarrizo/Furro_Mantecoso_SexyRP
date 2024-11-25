using Application.DataAccess;
using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    /// <summary>
    /// An instance of CarritoService.
    /// </summary>
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _CarritoRepository;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="repoC">An instance of the carrito repository.</param>
        public CarritoService(ICarritoRepository repoC)
        {
            _CarritoRepository = repoC;
        }

        /// <inheritdoc/>
        public async Task<Carrito?> GetCarritoByClientId(int id)
        {
            return await _CarritoRepository.GetCarritoByClientId(id);
        }

        /// <inheritdoc/>
        public async Task<Carrito?> GetCarritoById(Guid id)
        {
            return await _CarritoRepository.GetCarritoById(id);
        }

        /// <inheritdoc/>
        public async Task<Carrito?> UpdateCarrito(Carrito carrito)
        {
            return await _CarritoRepository.UpdateCarrito(carrito);
        }

        /// <inheritdoc/>
        public async Task<CarritoProducto?> DeleteCarritoProducto(CarritoProducto carrito)
        {
            return await _CarritoRepository.DeleteCarritoProducto(carrito);
        }

        /// <inheritdoc/>
        public async Task<CarritoProducto?> GetCarritoProductoById(Guid id, int productoId)
        {
            return await _CarritoRepository.GetCarritoProductoById(id, productoId);
        }

        /// <inheritdoc/>
        public async Task<Carrito> CreateCarrito(Carrito carrito)
        {
            return await _CarritoRepository.CreateCarrito(carrito);
        }

        /// <inheritdoc/>
        public async Task<CarritoProducto?> UpdateCarritoProducto(CarritoProducto carrito)
        {

            return await _CarritoRepository.UpdateCarritoProducto(carrito);
        }

        /// <inheritdoc/>
        public async Task<Carrito?> GetRawCarritoById(Guid id)
        {
            return await _CarritoRepository.GetRawCarritoById(id);
        }

        /// <inheritdoc/>
        public async Task<Carrito?> DeleteCarrito(Guid carritoId)
        {
            var carrito = await _CarritoRepository.GetRawCarritoById(carritoId); // Await here

            if (carrito != null)
            {
                return await _CarritoRepository.DeleteCarritoById(carritoId);
            }

            throw new FileNotFoundException("CarritoId : " + carritoId + " no encontrado");
        }
    }



}
