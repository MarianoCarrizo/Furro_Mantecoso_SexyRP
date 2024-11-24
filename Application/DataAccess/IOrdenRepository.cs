using Domain.Entities;

namespace Application.DataAccess
{
    public interface IOrdenRepository
    {
        public Orden CreateOrden(Orden orden);
        public List<Orden> GetOrderByPage(int limit, int page, DateTime? from = null, DateTime? to = null);
        public List<Orden> GetAllOrders(DateTime? from = null, DateTime? to = null);
    }
}
