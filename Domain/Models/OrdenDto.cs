using Domain.Models;

namespace Domain.Entities
{
    public class OrdenDto
    {
        public DateTime Fecha { get; set; }

        public int ClienteId { get; set; }

        public string ClienteDni { get; set; }
        public string ClienteNombre { get; set; }

        public decimal Total { get; set; }

        public List<ProductoDto> Productos { get; set; }

    }
}
