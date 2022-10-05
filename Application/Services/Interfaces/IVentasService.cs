using Domain.Entities;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IVentasService
    {
        public List<Venta> GetVentasByProduct(string codigo);

        void MostrarCarrito(Guid carritoId);
        public List<Venta> GetVentasByDay();

        public decimal SumarTotal(Carrito Carrito);
        public string CreateVenta();

        public void EliminarProductos(Guid cliente);

        public void AgregarProductos(Carrito carrito);





    }
}
