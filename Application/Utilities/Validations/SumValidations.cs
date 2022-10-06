using Domain.Entities;


namespace Application.Utilities.Validations
{
    public class SumValidations
    {
        public static decimal SumarTotal(Carrito Carrito)
        {
            decimal sum = 0;
            foreach (var pr in Carrito.CarritoProductos)
            {
                decimal productPrecio = pr.Producto.Precio * pr.Cantidad;
                sum += productPrecio;
            }
            return sum;
        }
    }
}
