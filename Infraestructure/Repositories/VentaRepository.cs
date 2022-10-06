using Application.DataAccess;
using Domain.Entities;
using Domain.Models;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly AppDbContext _context;

        public VentaRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public List<Venta> GetVentasByDay()
        {


            var carritos = _context.Carritos
                .Include(Carrito => Carrito.Cliente)
                .Include(Carrito => Carrito.Orden)
                .Include(Carrito => Carrito.CarritoProductos)
                .ThenInclude(CarritoProducto => CarritoProducto.Producto)
                .Where(Carrito => Carrito.Orden.Fecha.Date == DateTime.Now.Date)
                .ToList();
            var ventas = new List<Venta>();
            var productos = new List<Producto>();
            foreach (var carrito in carritos)
            {
                foreach (var CarritoProducto in carrito.CarritoProductos)
                {
                    productos.Add(CarritoProducto.Producto);
                }
                ventas.Add(new Venta
                {
                    Cliente = carrito.Cliente,
                    Productos = productos,
                    Fecha = carrito.Orden.Fecha,
                    Total = carrito.Orden.Total,

                });


                productos = new List<Producto>();
            }
            return ventas;
        }

        public List<Venta> GetVentasByProduct(string codigo)
        {
            var carritos = _context.Carritos
                .Include(Carrito => Carrito.Cliente)
                .Include(Carrito => Carrito.Orden)
                .Include(Carrito => Carrito.CarritoProductos)
                .ThenInclude(CarritoProducto => CarritoProducto.Producto)
                .ToList();
            var ventas = new List<Venta>();
            var productos = new List<Producto>();
            foreach (var carrito in carritos)
            {
                foreach (var CarritoProducto in carrito.CarritoProductos)
                {
                    if (CarritoProducto.Producto.Codigo == codigo)
                    {
                        productos.Add(CarritoProducto.Producto);
                        var sell = new Venta
                        {
                            Cliente = carrito.Cliente,
                            Productos = productos,
                            Fecha = carrito.Orden.Fecha,
                            Total = carrito.Orden.Total,
                        };
                        ventas.Add(sell);
                    }
                }


                productos = new List<Producto>();
            }
            return ventas;

        }
    }
}
