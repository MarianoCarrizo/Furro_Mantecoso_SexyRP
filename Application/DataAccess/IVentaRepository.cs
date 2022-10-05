using Domain.Models;

namespace Application.DataAccess
{
    public interface IVentaRepository
    {

        public List<Venta> GetVentasByProduct(string codigo);


        public List<Venta> GetVentasByDay();
    }
}
