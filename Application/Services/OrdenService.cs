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

    }
}
