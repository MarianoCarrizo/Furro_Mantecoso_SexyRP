using Domain.Entities;
using Domain.Models;

namespace Application.DataAccess
{
    public interface IProductRepository
    {

        public List<Producto> GetProductos();

          public Task<List<Producto>> GetProductos(string? name = null, bool? sort = null);
        public Producto GetProductById(int id);
    }
}
