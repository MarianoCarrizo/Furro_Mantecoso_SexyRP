using Application.DataAccess;

using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;


        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Producto FindProductById(int id)
        {
            return _repository.GetProductById(id);
        }

        public List<Producto> ShowProducts()
        {
            List<Producto> list = _repository.GetProductos();
            return list;
        }
    }
}
