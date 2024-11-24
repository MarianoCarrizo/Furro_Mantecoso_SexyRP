using Domain.Models;

namespace Domain.Entities
{
    public class CarritoProductoDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public virtual ProductoCarritoDTO Producto { get; set; } = null!;
    }
}
