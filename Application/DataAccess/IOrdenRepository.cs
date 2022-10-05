using Domain.Entities;

namespace Application.DataAccess
{
    public interface IOrdenRepository
    {

        public List<Orden> GetOrdenesByDay();


        public Orden CreateOrden(Orden orden);
    }
}
