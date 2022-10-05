namespace Domain.Entities
{
    public class Orden
    {
        public Guid OrdenId { get; set; }
        public Guid CarritoId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public virtual Carrito Carrito { get; set; } = null!;
    }
}
