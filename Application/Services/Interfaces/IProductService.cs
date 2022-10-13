using Domain.Entities;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductoFindDto> FindProductById(int id);
        Task<Producto> FindRawProductById(int id);

        Task<List<ProductoDto>> GetProducts(string? name = null, bool? sort = null);
    }
}
