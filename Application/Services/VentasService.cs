using Application.DataAccess;
using Application.Services.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class VentasService : IVentasService
    {
        private readonly IVentaRepository _VentaRepository;


        public VentasService(
            IVentaRepository repoV
        )
        {
            _VentaRepository = repoV;

        }


        public List<Venta> GetVentasByDay()
        {
            return _VentaRepository.GetVentasByDay();
        }

        public List<Venta> GetVentasByProduct(string codigo)
        {
            return _VentaRepository.GetVentasByProduct(codigo);
        }
    }
}
