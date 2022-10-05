using Domain.Entities;

namespace Domain.Models
{
    public class Venta
    {
        public Cliente Cliente { get; set; }
        public List<Producto> Productos { get; set; }
        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

    }
}
