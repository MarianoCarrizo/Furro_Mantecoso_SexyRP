using Application.DataAccess;
using Domain.Entities;
using Infraestructure.Persistance;

namespace Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Task<List<Producto>> GetProductos(string? name = null, bool? sort = null)
        {
            if (sort == false)
            {
                var Productos = _context.Productos.OrderByDescending(Producto => Producto.Precio).Where(Product => string.IsNullOrEmpty(name) || Product.Nombre.Contains(name) == true).ToList();
                return Task.FromResult(Productos);
            }
            else
            {
                var Productos = _context.Productos.OrderBy(Producto => Producto.Precio).Where(Product => string.IsNullOrEmpty(name) || Product.Nombre.Contains(name) == true).ToList();
                return Task.FromResult(Productos);
            }

        }


        public Task<Producto> GetProductById(int id)
        {
            return Task.FromResult(_context.Productos.FirstOrDefault(product => product.ProductoId == id));
        }

        public List<Producto> GetProductos()
        {
            return _context.Productos.ToList();
        }
    }
}
