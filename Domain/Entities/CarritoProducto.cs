namespace Domain.Entities
{
    public class CarritoProducto
    {
        public Guid CarritoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public virtual Carrito Carrito { get; set; } = null!;
        public virtual Producto Producto { get; set; } = null!;
    }
}
