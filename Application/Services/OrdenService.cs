using Application.DataAccess;

using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Application.Services
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _repository;
        private readonly IMapper _mapper;


        public OrdenService(IOrdenRepository repository, IMapper mapper)
        {
            _repository = repository;
               _mapper = mapper;
          }

        public Orden CreateOrden(Orden order)
        {
            return _repository.CreateOrden(order);
        }

          public async Task<List<OrdenDto>> GetOrder(DateTime? from = null, DateTime? to = null)
          {
              var order = _repository.GetOrder(from, to);
               List<OrdenDto> ordenes = new List<OrdenDto>();
               foreach (var item in order)
               {
                  var ordenMapeada = _mapper.Map<OrdenDto>(item);
                    ordenes.Add(ordenMapeada);
               }
               return ordenes;
          }
     }
}
