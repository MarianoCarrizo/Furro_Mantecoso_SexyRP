using Domain.Entities;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Producto FindProductById(int id);

          Task<List<ProductoDto>> GetProducts(string? name = null, bool? sort = null);
          List<Producto> ShowProducts();
    }
}
