using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IVentasService
    {
        public List<Venta> GetVentasByProduct(string codigo);

        public List<Venta> GetVentasByDay();

    }
}
