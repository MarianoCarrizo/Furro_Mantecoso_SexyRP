using Domain.Entities;

namespace Domain.Models
{
    public class CompraCarritoDto
     {
        public int ClienteId { get; set; }
        public int ProductoId{ get; set; }

        public int cantidad  { get; set; }

    }
}
