
using Domain.Entities;

namespace Domain.model

{
    public class CarritoDto
    {
        public CarritoDto()
        {
            CarritoProductos = new HashSet<CarritoProductoDTO>();
        }

        public Guid CarritoId { get; set; }
        public int ClienteId { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<CarritoProductoDTO> CarritoProductos { get; set; }
    }
}
