using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Producto FindProductById(int id);


        List<Producto> ShowProducts();
    }
}
