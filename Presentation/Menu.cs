using Application.Services.Interfaces;
using Application.Utilities.Validations;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public class Menu
    {
        private readonly IServiceProvider _serviceProvider;

        public Menu(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void StartMenu()
        {

            Console.Clear();
            Console.WriteLine("**************************************");
            Console.WriteLine("*        SUPERMERCADOS NOCHE         *");
            Console.WriteLine("*                                    *");
            Console.WriteLine("**************************************");
            Console.WriteLine("*                                    *");
            Console.WriteLine("**************************************");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Selecciones una opción.");
            Console.WriteLine("");
            Console.WriteLine(" 1 ) -  Registrar Clientes");
            Console.WriteLine(" 2 ) - Registrar Venta");
            Console.WriteLine(" 3 ) - Reportes de ventas del día");
            Console.WriteLine(" 4 ) - Reportes de ventas de un producto ");
            Console.WriteLine(" 5 ) - Observar Carrito              ");
            Console.WriteLine(" 6 ) - Salir del sistema");
            int select = InputValidations.IntInput("Ingrese su seleccion: ", 1, 7);
            while (select != 7)
            {
                switch (select)
                {
                    case 1:
                        Console.WriteLine();
                        RegistrarCliente(_serviceProvider.GetRequiredService<IClienteService>());
                        Console.WriteLine("Presione cualquier tecla para volver al menu. ");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine();
                        RegistrarVenta(_serviceProvider.GetRequiredService<IVentasService>());
                        Console.WriteLine("Presione cualquier tecla para volver al menu. ");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine();
                        MostrarVentasPorDia(_serviceProvider.GetRequiredService<IVentasService>());
                        Console.WriteLine("Presione cualquier tecla para volver al menu. ");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine();
                        MostrarVentasPorProducto(_serviceProvider.GetRequiredService<IVentasService>(), _serviceProvider.GetRequiredService<IProductService>());
                        Console.WriteLine("Presione cualquier tecla para volver al menu. ");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.WriteLine();
                        MostrarCarrito(_serviceProvider.GetRequiredService<IVentasService>(), _serviceProvider.GetRequiredService<IClienteService>(), _serviceProvider.GetRequiredService<ICarritoService>());
                        Console.WriteLine("Presione cualquier tecla para volver al menu. ");
                        Console.ReadKey();
                        break;
                    case 6:
                        break;
                    default: Console.WriteLine("Opcion invalida"); break;
                }
                if (InputValidations.IsValid(select) == true) { select = 7; }
                else
                {
                    Console.Clear();
                    Console.WriteLine("**************************************");
                    Console.WriteLine("*        SUPERMERCADOS NOCHE         *");
                    Console.WriteLine("*                                    *");
                    Console.WriteLine("**************************************");
                    Console.WriteLine("*                                    *");
                    Console.WriteLine("**************************************");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Selecciones una opción.");
                    Console.WriteLine("");
                    Console.WriteLine(" 1 ) -  Registrar Clientes");
                    Console.WriteLine(" 2 ) - Registrar Venta");
                    Console.WriteLine(" 3 ) - Reportes de ventas del día");
                    Console.WriteLine(" 4 ) - Reportes de ventas de un producto ");
                    Console.WriteLine(" 5 ) - Observar Carrito              ");
                    Console.WriteLine(" 6 ) - Salir del sistema");
                    select = InputValidations.IntInput("Ingresar : ", 1, 7);
                }
            }

        }
        private static void RegistrarCliente(IClienteService service)
        {
            Console.Clear();

            try
            {

                Console.WriteLine("NUEVO CLIENTE");
                string dni = InputValidations.DniInput("inserte DNI de cliente : ");

                string nombre = InputValidations.StringInput("Inserte nombre de cliente  : ");
                string apellido = InputValidations.StringInput("Inserte apellido de cliente  : ");
                string direccion = InputValidations.StringInput("Inserte direccion de cliente  : ");
                string telefono = InputValidations.StringInput("Inserte telefono de cliente  : ");
                if (dni.Contains("error,No puede ingresarse un DNI Vacio"))
                {
                    Console.WriteLine(" No se puede ingresar un DNI VACIO!");
                }
                else
                {
                    if (service.GetClienteByDNI(dni) != null)
                    {
                        Console.WriteLine("El dni ingresado es de un cliente existente");
                    }
                    else
                    {
                        var ClienteCreado = service.CreateClient(dni, nombre, apellido, direccion, telefono);
                    }


                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error, ingreso mal un valor.");
            }

        }



        private static void RegistrarVenta(IVentasService service)
        {
            service.CreateVenta();
            Console.ReadLine();

        }

        private static void MostrarCarrito(IVentasService service, IClienteService service1, ICarritoService service3)
        {
            Console.Clear();
            Console.WriteLine("Lista de Clientes  : ");
            List<Cliente> client = service1.ShowClientes();

            foreach (var pr in client)
            {

                Console.WriteLine("{0} ) || nombre: {1} || apellido: {2} ", pr.ClienteId, pr.Nombre, pr.Apellido);
                Console.WriteLine("");
            }
            int select = InputValidations.IntInput("Ingresar : ", 1, client.Count);
            Cliente carrito = service1.GetClienteById(select);
            Carrito carro = service3.GetCarritoByClientId(carrito.ClienteId);
            if (carro != null)
            {
                if (carro.Estado == true)
                {
                    service.MostrarCarrito(carro.CarritoId);

                }
                Console.WriteLine("El carrito está vacio!");
            }
            Console.WriteLine("El carrito está vacio!");


        }



        public static void MostrarVentasPorDia(IVentasService service)
        {
            Console.Clear();
            var ventas = service.GetVentasByDay();
            int count = 1;
            foreach (var venta in ventas)
            {
                Console.WriteLine("{0} ) || Fecha: {1} || Total: {2} ", count, venta.Fecha, venta.Total);
                Console.WriteLine("");
                count++;
            }
        }
        public static void MostrarVentasPorProducto(IVentasService service,IProductService service2)
        {
            Console.Clear();
            var products = service2.ShowProducts();
            foreach(var pr in products)
            {
                Console.WriteLine(
                    "{0} ) || codigo: {1} || precio: {2} || marca: {3} || descripcion: {4}",
                    pr.ProductoId,
                    pr.Codigo,
                    pr.Precio,
                    pr.Marca,
                    pr.Descripcion
                );
                Console.WriteLine("");
            }
            string codigo = InputValidations.StringInput(
                "ingrese Codigo de producto para ver las ventas relacionadas : "
            );
            var ventas = service.GetVentasByProduct(codigo);
            if (ventas.Count > 0)
            {
                int count = 1;
                foreach (var venta in ventas)
                {
                    Console.WriteLine("{0} ) || Fecha: {1} || Total: {2} ||", count, venta.Fecha, venta.Total);
                    Console.WriteLine("");
                    count++;
                }
            }
            else
            {
                Console.WriteLine("No existen ventas relacionadas a ese producto.");
            }

        }


    }
}