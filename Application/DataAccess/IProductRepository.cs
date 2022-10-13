using Domain.Entities;
using Domain.Models;

namespace Application.DataAccess
{
    public interface IProductRepository
    {
        public Task<List<Producto>> GetProductos(string? name = null, bool? sort = null);
        public Task<Producto> GetProductById(int id);
    }
}
