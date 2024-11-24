using Application.DataAccess;
using Domain.Entities;
using Domain.Models;
using Infraestructure.Persistance;
using System.Xml.Linq;

namespace Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Task<List<Producto>> GetProductos(string? category, string? name = null, bool? sort = null)
        {
            var query = _context.Productos.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Categoria.Contains(category)); // Match category
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Nombre.Contains(name)); // Match name
            }
            if (sort.HasValue)
            {
                query = sort.Value
                    ? query.OrderBy(p => p.Precio)  // Ascending order by Precio
                    : query.OrderByDescending(p => p.Precio); // Descending order by Precio
            }
            else
            {
                query = query.OrderBy(p => p.Precio);
            }

            var productos = query.ToList();
            return Task.FromResult(productos);
        }



        public Task<Producto> GetProductById(int id)
        {
            return Task.FromResult(_context.Productos.FirstOrDefault(product => product.ProductoId == id));
        }

        public List<Producto> GetProductos()
        {
            return _context.Productos.ToList();
        }

        public Task<List<Producto>> GetProductosByCategoryOrBrand(string? category, string? brand)
        {
            // Start by querying the productos
            var query = _context.Productos.AsQueryable();

            // If a category is provided, filter by category
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Categoria.Contains(category));
            }

            // If a brand is provided, filter by brand
            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(p => p.Marca.Contains(brand));
            }

            // Execute the query and return the result as a list
            var productos = query.ToList();

            return Task.FromResult(productos);
        }


    }
}
