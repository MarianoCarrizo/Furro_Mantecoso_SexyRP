using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IVentaService
    {
        public List<Venta> GetVentasByProduct(string codigo);

        public List<Venta> GetVentasByDay();

    }
}
