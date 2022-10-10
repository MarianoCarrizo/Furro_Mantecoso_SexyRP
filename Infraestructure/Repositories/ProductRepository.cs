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

          public Producto GetProductById(int id)
          {
               return _context.Productos.FirstOrDefault(product => product.ProductoId == id);
          }

          public List<Producto> GetProductos()
          {
               return _context.Productos.ToList();
          }
     }
}
