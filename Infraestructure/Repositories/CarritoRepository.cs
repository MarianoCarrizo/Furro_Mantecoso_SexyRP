using Application.DataAccess;
using Domain.Entities;
using Infraestructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// The constructor of the CarritoRepository.
        /// </summary>
        /// <param name="appDbContext">An instance of the Db Context.</param>
        public CarritoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext ?? throw new NullReferenceException(nameof(appDbContext));
        }

        /// <inheritdoc/>
        public async Task<Carrito> CreateCarrito(Carrito carrito)
        {
            await _context.Carritos.AddAsync(carrito);
            await _context.SaveChangesAsync();
            return carrito;
        }

        /// <inheritdoc/>
        public async Task<CarritoProducto?> GetCarritoProductoById(Guid id, int Id)
        {
            return await _context.CarritoProductos.FirstOrDefaultAsync(d => d.CarritoId == id && d.ProductoId == Id);
        }

        /// <inheritdoc/>
        public async Task<Carrito?> GetCarritoByClientId(int Id)
        {
            return await _context.Carritos.Include(Carrito => Carrito.CarritoProductos)
                                    .ThenInclude(carro => carro.Producto)
                                    .FirstOrDefaultAsync(d => d.ClienteId.Equals(Id) && d.Estado == true);
        }

        /// <inheritdoc/>
        public async Task<Carrito?> GetCarritoById(Guid id)
        {
            return await _context.Carritos.Include(Carrito => Carrito.CarritoProductos)
                                    .ThenInclude(carro => carro.Producto)
                                    .FirstOrDefaultAsync(d => d.CarritoId.Equals(id) && d.Estado == true);
        }

        /// <inheritdoc/>
        public async Task<CarritoProducto?> DeleteCarritoProducto(CarritoProducto carritoProducto)
        {
            _context.CarritoProductos.Remove(carritoProducto);
            await _context.SaveChangesAsync();
            return carritoProducto;
        }

        /// <inheritdoc/>
        public async Task<Carrito?> UpdateCarrito(Carrito Carrito)
        {
            _context.Update(Carrito);
            await _context.SaveChangesAsync();
            return Carrito;
        }

        /// <inheritdoc/>
        public async Task<CarritoProducto?> UpdateCarritoProducto(CarritoProducto carrito)
        {
            _context.CarritoProductos.Update(carrito);
            await _context.SaveChangesAsync();
            return carrito;
        }

        /// <inheritdoc/>
        public async Task<Carrito?> GetRawCarritoById(Guid id)
        {
            return await _context.Carritos.Include(Carrito => Carrito.CarritoProductos)
                                 .ThenInclude(carro => carro.Producto)
                                 .FirstOrDefaultAsync(d => d.CarritoId.Equals(id));
        }

        /// <inheritdoc/>
        public async Task<Carrito?> DeleteCarritoById(Guid CarritoId)
        {
            var carrito = await _context.Carritos.FirstOrDefaultAsync(c => c.CarritoId == CarritoId);

            if (carrito != null)
            {
                // Remove the found Carrito from the context
                _context.Carritos.Remove(carrito);
                _context.SaveChanges();
            }

            return carrito;
        }
    }
}
