using Application.DataAccess;
using Application.Services.Interfaces;
using Application.Utilities.Validations;
using Domain.Entities;
using Domain.Models;

namespace Application.Services
{
    public class VentasService : IVentasService
    {
        private readonly IVentaRepository _VentaRepository;
        private readonly ICarritoRepository _CarritoRepository;
        private readonly IClienteRepository _ClienteRepository;
        private readonly IOrdenRepository _OrdenRepository;
        private readonly IProductRepository _ProductRepository;

        public VentasService(
            IVentaRepository repoV,
            ICarritoRepository repoC,
            IClienteRepository repoClient,
            IOrdenRepository repoO,
            IProductRepository repoP
        )
        {
            _VentaRepository = repoV;
            _CarritoRepository = repoC;
            _ClienteRepository = repoClient;
            _OrdenRepository = repoO;
            _ProductRepository = repoP;
        }

        public string CreateVenta()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Nueva venta");

                List<Cliente> clients = _ClienteRepository.GetAllClientes();
                Console.WriteLine("Lista de Clientes  : ");
                foreach (var cl in clients)
                {
                    Console.WriteLine("{0} ) || nombre: {1} || apellido: {2} ",
                        cl.ClienteId,
                        cl.Nombre,
                        cl.Apellido
                    );
                    Console.WriteLine("");
                }
                int client = InputValidations.IntInput(
                    "Ingrese el id de cliente que desea comprar : ",
                    1,
                    clients.Count
                );
                var cliente = _ClienteRepository.GetClienteById(client); // buscamos cliente.
                var carritoCliente = new Carrito()
                {
                    CarritoId = Guid.NewGuid(),
                    Estado = true,
                    ClienteId = cliente.ClienteId,
                };
                string ask = InputValidations.YesOrNoInput("Desea agregar Productos? (Si o No): ");
                if (ask == "si")
                {
                     AgregarProductos(carritoCliente);
                }
                string modify = InputValidations.YesOrNoInput(
                    "Desea Eliminar Productos? (Si o No): "
                );
                if (modify == "si")
                {
                    EliminarProductos(carritoCliente.CarritoId);
                }

                string stillBuying = InputValidations
                    .YesOrNoInput("Desea confirmar la compra? responder con Sí o No : ");
                carritoCliente = _CarritoRepository.GetCarritoById(carritoCliente.CarritoId);
                if (stillBuying == "si")
                {
                    var orden = new Orden
                    {
                        OrdenId = Guid.NewGuid(),
                        CarritoId = carritoCliente.CarritoId,
                        Fecha = DateTime.Now,
                        Total = SumarTotal(carritoCliente),
                    };
                    _OrdenRepository.CreateOrden(orden);
                    carritoCliente.Estado = false;
                    _CarritoRepository.UpdateCarrito(carritoCliente);

                    return "Se ha efectuado la compra";
                }
                else
                {
                    return "compra cancelada";
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void MostrarCarrito(Guid cliente)
        {
            Console.Clear();
            var carritoContent = _CarritoRepository.GetCarritoById(cliente);
            if (carritoContent.CarritoProductos.Count != 0)
            {
                Console.WriteLine("MOSTRANDO CARRITO");
                int count = 1;
                foreach (var pr in carritoContent.CarritoProductos)
                {
                    Console.WriteLine(
                        "{0} ) || producto: {1} || precio: {2} || marca: {3} || descripcion: {4}",
                        count,
                        pr.Producto.Nombre,
                        pr.Producto.Precio,
                        pr.Producto.Marca,
                        pr.Producto.Descripcion
                    );
                    Console.WriteLine("");
                    count++;
                }
            }
            else
            {
                Console.WriteLine("el carrito está vacio... ");
            }
        }

        public decimal SumarTotal(Carrito Carrito)
        {
            decimal sum = 0;
            foreach (var pr in Carrito.CarritoProductos)
            {
                decimal productPrecio = pr.Producto.Precio * pr.Cantidad;
                sum += productPrecio;
            }
            return sum;
        }

        public void AgregarProductos(Carrito carrito)
        {
            try
            {
                _CarritoRepository.CreateCarrito(carrito);
                Console.Clear();
                Console.WriteLine("Agregar productos");
                Console.Clear();
                Console.WriteLine("Mostrando Lista de productos disponibles ...");
                List<Producto> products = _ProductRepository.GetProductos();
                foreach (var pr in products)
                {
                    Console.WriteLine(
                        "{0} ) || producto: {1} || precio: {2} || marca: {3} || descripcion: {4}",
                        pr.ProductoId,
                        pr.Nombre,
                        pr.Precio,
                        pr.Marca,
                        pr.Descripcion
                    );
                    Console.WriteLine("");
                }
                string stillBuying = "si";
                while (stillBuying == "si")
                {
                    int product = InputValidations.IntInput(
                        "Ingrese el id del producto a comprar : ",
                        1,
                        products.Count
                    );
                    int cantidad = InputValidations.IntInput(
                        "Ingrese cantidad del producto a comprar : ",
                        1,
                        999999999
                    );
                    var carritoProduct = new CarritoProducto()
                    {
                        CarritoId = carrito.CarritoId,
                        Cantidad = cantidad,
                        ProductoId = product,
                    };
                    carrito.CarritoProductos.Add(carritoProduct);
                    _CarritoRepository.CreateCarritoProducto(carritoProduct);
                    stillBuying = InputValidations
                        .YesOrNoInput("desea seguir comprando? responder con Sí o No : ");
                }
                
                _CarritoRepository.UpdateCarrito(carrito);
            }
            catch (Exception)
            {
                
            }
        }

        public void EliminarProductos(Guid CarritoId)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("eliminar productos");
                var carrito = _CarritoRepository.GetCarritoById(CarritoId); //buscamos carrito
                bool IsEmpty = false;
                if (carrito.CarritoProductos.Count == 0) //
                {
                    Console.WriteLine(
                        "el carrito esta vacio! porfavor ingrese algún producto primero."
                    );
                    IsEmpty = true;
                }
                else
                {
                    MostrarCarrito(CarritoId);
                }
                if (IsEmpty == false)
                {
                    bool stillBuying = true;

                    while (stillBuying == true)
                    {
                        if (carrito.CarritoProductos.Count != 0)
                        {
                            int product = InputValidations.IntInput(
                                "Ingrese el id del producto a eliminar : ",
                                1,
                                carrito.CarritoProductos.Count
                            );
                            _CarritoRepository.DeleteCarritoProducto(
                                _CarritoRepository.GetCarritoProductoById(
                                    carrito.CarritoId,
                                    product
                                )
                            );
                            string ask = InputValidations
                                .YesOrNoInput("desea seguir eliminando? responder con Sí o No : ");
                            if (ask == "no")
                            {
                                stillBuying = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine(
                                "El Carrito está vacio! ingrese algo primero para eliminarlo!"
                            );
                            stillBuying = false;
                        }
                    }
                    ;
                    _CarritoRepository.UpdateCarrito(carrito);
                }
            }
            catch (Exception) { }
        }

        public List<Venta> GetVentasByDay()
        {
            return _VentaRepository.GetVentasByDay();
        }

        public List<Venta> GetVentasByProduct(string codigo)
        {
            return _VentaRepository.GetVentasByProduct(codigo);
        }
    }
}
