using Application.DataAccess;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infrastructure.Repositories
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly AppDbContext _context;

        public OrdenRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Orden CreateOrden(Orden orden)
        {
            _context.Ordenes.Add(orden);
            _context.SaveChanges();

            return orden;
        }



        public List<Orden> GetOrdenesByDay()
        {
            return _context.Ordenes.Where(o => o.Fecha.Date == DateTime.Now.Date).ToList();
        }

    }
}
