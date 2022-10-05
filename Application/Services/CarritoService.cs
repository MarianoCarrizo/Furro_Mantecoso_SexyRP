using Application.DataAccess;
using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _CarritoRepository;

        public CarritoService(ICarritoRepository repoC)
        {
            _CarritoRepository = repoC;
        }

        public Carrito GetCarritoByClientId(int id)
        {
            return _CarritoRepository.GetCarritoByClientId(id);
        }

        public Carrito UpdateCarrito(Carrito carrito)
        {
            return _CarritoRepository.UpdateCarrito(carrito);
        }
    }



}
