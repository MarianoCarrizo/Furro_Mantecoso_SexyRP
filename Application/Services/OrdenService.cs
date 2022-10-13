using Application.DataAccess;

using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _repository;


        public OrdenService(IOrdenRepository repository)
        {
            _repository = repository;
        }

        public Orden CreateOrden(Orden order)
        {
            return _repository.CreateOrden(order);
        }

        public async Task<List<Orden>> GetOrder(DateTime? from = null, DateTime? to = null)
        {
            return _repository.GetOrder(from, to);

        }
    }
}
