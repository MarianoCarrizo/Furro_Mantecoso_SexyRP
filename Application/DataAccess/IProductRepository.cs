using Domain.Entities;
using Domain.Models;

namespace Application.DataAccess
{
    public interface IProductRepository
    {
        public Task<List<Producto>> GetProductos(string? category, string? name = null, bool? sort = null);

        public Task<List<Producto>> GetProductosByCategoryOrBrand(string? category, string? brand);
        public Task<Producto> GetProductById(int id);
    }
}
