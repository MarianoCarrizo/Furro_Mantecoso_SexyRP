using Application.DataAccess;

using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using System.Xml.Linq;


namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;



        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<ProductoFindDto> FindProductById(int id)
        {

            var product = _repository.GetProductById(id).Result;
            var producto = new ProductoFindDto()
            {
                Nombre = product.Nombre,
                Descripcion = product.Descripcion,
                Marca = product.Marca,
                Codigo = product.Codigo,
                Image = product.Image,
                Precio = product.Precio
            };

            return producto;

        }

        public async Task<Producto> FindRawProductById(int id)
        {

            var product = _repository.GetProductById(id).Result;
            return product;

        }



        public Task<List<ProductoFindDto>> GetProducts(string? category, string? name = null, bool? sort = null)
        {
            var product = _repository.GetProductos(category,name, sort);
            var lista = new List<ProductoFindDto>();
            foreach (Producto pro in product.Result)
            {
                var producto = new ProductoFindDto()
                {
                    ProductoId = pro.ProductoId,
                    Nombre = pro.Nombre,
                    Precio = pro.Precio,
                    Codigo = pro.Codigo,
                    Categoria = pro.Categoria,
                    Descripcion = pro.Descripcion,
                    Marca = pro.Marca,
                    Image = pro.Image
                };
                lista.Add(producto);
            }
            return Task.FromResult(lista);
        }

        public async Task<List<ProductoFindDto>> GetProductosByCategoryOrBrand(string? category, string? brand)
        {
            var product = _repository.GetProductosByCategoryOrBrand(category,brand);
            var lista = new List<ProductoFindDto>();
            foreach (Producto pro in product.Result)
            {
                var producto = new ProductoFindDto()
                {
                    ProductoId = pro.ProductoId,
                    Nombre = pro.Nombre,
                    Precio = pro.Precio,
                    Codigo = pro.Codigo,
                    Categoria = pro.Categoria,
                    Descripcion = pro.Descripcion,
                    Marca = pro.Marca,
                    Image = pro.Image
                };
                lista.Add(producto);
            }
            return lista;
        }
    }
}
