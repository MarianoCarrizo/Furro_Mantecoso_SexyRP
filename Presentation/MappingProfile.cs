using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace TP.Application.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
               CreateMap<Cliente, ClienteDto>();
               CreateMap<Producto, ProductoDto>().ReverseMap();
               CreateMap<Venta, VentaDtoForDisplay>();
        

          }
    }
}
