using Domain.Entities;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductoFindDto> FindProductById(int id);

        Task<Producto> FindRawProductById(int id);

        Task <List<ProductoFindDto>> GetProducts(string? category, string? name = null, bool? sort = null);

        Task<List<ProductoFindDto>> GetProductosByCategoryOrBrand(string? category, string? brand);
    }
}
