using AutoMapper;
using Domain.Entities;
using Domain.model;
using Domain.Models;

namespace TP.Application.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Orden, OrdenDto>().ReverseMap();
            CreateMap<Carrito, CarritoDto>().ReverseMap();
            CreateMap<CarritoProducto, CarritoProductoDTO>().ReverseMap();
            CreateMap<Producto, ProductoCarritoDTO>().ReverseMap();


        }
    }
}
