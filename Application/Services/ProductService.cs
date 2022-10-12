using Application.DataAccess;

using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using System.Net;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
          private readonly IMapper _mapper;


        public ProductService(IProductRepository repository,IMapper mapper)
        {
            _repository = repository;
               _mapper = mapper;
        }

        public Producto FindProductById(int id)
        {
            return _repository.GetProductById(id);
        }

          public Task<List<ProductoDto>> GetProducts(string? name = null, bool? sort = null)
          {
               var product = _repository.GetProductos(name, sort);
               var lista = new List<ProductoDto>();   
               foreach( Producto pro in product.Result)
               {
                    var producto = new ProductoDto()
                    {
                        Nombre = pro.Nombre,
                        Precio = pro.Precio,
                    };
                    lista.Add(producto);
               }
               return Task.FromResult(lista);
          }

          public List<Producto> ShowProducts()
        {
            List<Producto> list = _repository.GetProductos();
            return list;
        }
    }
}
