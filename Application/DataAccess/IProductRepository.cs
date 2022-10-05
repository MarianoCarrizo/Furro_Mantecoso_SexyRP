using Domain.Entities;

namespace Application.DataAccess
{
    public interface IProductRepository
    {

        public List<Producto> GetProductos();
        public Producto GetProductById(int id);
    }
}
