using Domain.Models;

namespace Domain.Entities
{
    public class OrdenDto
    {
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public List<ProductoDto> Productos { get; set; }

    }
}
