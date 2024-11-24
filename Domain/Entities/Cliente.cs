namespace Domain.Entities
{
    public class Cliente
    {
        public Cliente()
        {
            Carritos = new HashSet<Carrito>();
        }

        public int ClienteId { get; set; }
        public string Dni { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        public virtual ICollection<Carrito> Carritos { get; set; }
    }
}
