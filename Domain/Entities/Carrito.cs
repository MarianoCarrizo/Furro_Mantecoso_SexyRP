namespace Domain.Entities
{
    public class Carrito
    {
        public Carrito()
        {
            CarritoProductos = new HashSet<CarritoProducto>();
        }

        public Guid CarritoId { get; set; }
        public int ClienteId { get; set; }
        public bool Estado { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Orden? Orden { get; set; }
        public virtual ICollection<CarritoProducto> CarritoProductos { get; set; }
    }
}
