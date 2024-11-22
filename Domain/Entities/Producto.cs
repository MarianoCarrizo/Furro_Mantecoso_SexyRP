namespace Domain.Entities
{
    public class Producto
    {
        public Producto()
        {
            CarritoProductos = new HashSet<CarritoProducto>();
        }

        public int ProductoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Categoria { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Image { get; set; } = null!;

        public virtual ICollection<CarritoProducto> CarritoProductos { get; set; }
    }
}
