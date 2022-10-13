using Application.DataAccess;
using Domain.Entities;
using Infraestructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly AppDbContext _context;

        public CarritoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Carrito CreateCarrito(Carrito carrito)
        {
            _context.Carritos.Add(carrito);
            _context.SaveChanges();
            return carrito;
        }
        public CarritoProducto GetCarritoProductoById(Guid id, int Id)
        {
            return _context.CarritoProductos.FirstOrDefault(d => d.CarritoId == id && d.ProductoId == Id);
        }

        public Carrito GetCarritoByClientId(int Id)
        {


            return _context.Carritos.Include(Carrito => Carrito.CarritoProductos)
                                    .ThenInclude(carro => carro.Producto)
                                    .FirstOrDefault(d => d.ClienteId.Equals(Id) && d.Estado == true);
        }

        public Carrito GetCarritoById(Guid id)
        {
            return _context.Carritos.Include(Carrito => Carrito.CarritoProductos)
                                    .ThenInclude(carro => carro.Producto)
                                    .FirstOrDefault(d => d.CarritoId.Equals(id) && d.Estado == true);
        }

        public CarritoProducto DeleteCarritoProducto(CarritoProducto carritoProducto)
        {
            _context.CarritoProductos.Remove(carritoProducto);
            _context.SaveChanges();
            return carritoProducto;
        }


        public Carrito UpdateCarrito(Carrito Carrito)
        {
            _context.Update(Carrito);
            _context.SaveChanges();
            return Carrito;
        }

        public CarritoProducto UpdateCarritoProducto(CarritoProducto carrito)
        {
            _context.CarritoProductos.Update(carrito);
            _context.SaveChanges();
            return carrito;
        }

        public Carrito GetRawCarritoById(Guid id)
        {
            return _context.Carritos.Include(Carrito => Carrito.CarritoProductos)
                                 .ThenInclude(carro => carro.Producto)
                                 .FirstOrDefault(d => d.CarritoId.Equals(id));
        }
    }
}
