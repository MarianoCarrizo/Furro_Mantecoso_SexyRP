using Application.DataAccess;
using Application.Services.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _VentaRepository;


        public VentaService(
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
