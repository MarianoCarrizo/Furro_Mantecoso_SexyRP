using Application.Services.Interfaces;
using Application.Utilities.Validations;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
     public class DeadMenu
     {

          private static void RegistrarCliente(IClienteService clienteService)
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
                         if (clienteService.GetClienteByDNI(dni) != null)
                         {
                              Console.WriteLine("El dni ingresado es de un cliente existente");
                         }
                         else
                         {
                              var ClienteCreado = clienteService.CreateClient(
                                  dni,
                                  nombre,
                                  apellido,
                                  direccion,
                                  telefono
                              );
                              Console.WriteLine("se ha creado nuevo cliente");
                         }
                    }
               }
               catch (Exception)
               {
                    Console.WriteLine("Error, ingreso mal un valor.");
               }
          }

          private static void RegistrarVenta(
              IClienteService clienteService,
              ICarritoService carritoService,
              IOrdenService ordenService,
              IProductService productService
          )
          {
               Console.Clear();
               bool stillBuying = true;
               Console.WriteLine("Nueva venta");
               List<Cliente> clients = clienteService.ShowClientes();
               Console.WriteLine("Lista de Clientes  : ");
               foreach (var cl in clients)
               {
                    Console.WriteLine(
                        "{0} ) || nombre: {1} || apellido: {2} ",
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
               var cliente = clienteService.GetClienteById(client); // buscamos cliente.
               var carritoCliente = new Carrito()
               {
                    CarritoId = Guid.NewGuid(),
                    Estado = true,
                    ClienteId = cliente.ClienteId,
               };
               string ask = InputValidations.YesOrNoInput("Desea agregar Productos? (Si o No): ");
               if (ask == "si")
               {
                    carritoService.CreateCarrito(carritoCliente);
                    Console.Clear();
                    Console.WriteLine("Agregar productos");
                    Console.Clear();
                    Console.WriteLine("Mostrando Lista de productos disponibles ...");
                    List<Producto> products = productService.ShowProducts();
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
                    string stillBuying2 = "si";
                    while (stillBuying2 == "si")
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
                              CarritoId = carritoCliente.CarritoId,
                              Cantidad = cantidad,
                              ProductoId = product,
                         };
                         carritoCliente.CarritoProductos.Add(carritoProduct);
                         carritoService.CreateCarritoProducto(carritoProduct);
                         stillBuying2 = InputValidations.YesOrNoInput(
                             "desea seguir comprando? responder con Sí o No : "
                         );
                    }

                    carritoService.UpdateCarrito(carritoCliente);
               }
               string modify = InputValidations.YesOrNoInput("Desea Eliminar Productos? (Si o No): ");
               if (modify == "si")
               {
                    Console.Clear();
                    Console.WriteLine("eliminar productos");
                    bool IsEmpty = false;
                    if (carritoCliente.CarritoProductos.Count == 0) //
                    {
                         Console.WriteLine(
                             "el carrito esta vacio! porfavor ingrese algún producto primero."
                         );
                         IsEmpty = true;
                    }
                    else
                    {
                         if (carritoCliente.CarritoProductos.Count != 0)
                         {
                              Console.WriteLine("MOSTRANDO CARRITO");
                              int count = 1;
                              foreach (var pr in carritoCliente.CarritoProductos)
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
                    if (IsEmpty == false)
                    {
                         while (stillBuying == true)
                         {
                              if (carritoCliente.CarritoProductos.Count != 0)
                              {
                                   int product = InputValidations.IntInput(
                                       "Ingrese el id del producto a eliminar : ",
                                       1,
                                       carritoCliente.CarritoProductos.Count
                                   );
                                   carritoService.DeleteCarritoProducto(
                                       carritoService.GetCarritoProductoById(carritoCliente.CarritoId, product)
                                   );
                                   string askDelete = InputValidations.YesOrNoInput(
                                       "desea seguir eliminando? responder con Sí o No : "
                                   );
                                   if (askDelete == "no")
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
                        
                         carritoService.UpdateCarrito(carritoCliente);
                    }
               }
               string stillBuyingValidate = InputValidations.YesOrNoInput(
                   "Desea confirmar la compra? responder con Sí o No : "
               );
               carritoCliente = carritoService.GetCarritoById(carritoCliente.CarritoId);
               if (stillBuyingValidate == "si")
               {
                    var orden = new Orden
                    {
                         OrdenId = Guid.NewGuid(),
                         CarritoId = carritoCliente.CarritoId,
                         Fecha = DateTime.Now,
                         Total = SumValidations.SumarTotal(carritoCliente)
                    };
                    ordenService.CreateOrden(orden);
                    carritoCliente.Estado = false;
                    carritoService.UpdateCarrito(carritoCliente);
                    Console.WriteLine("Se ha completado la compra");
                    Console.ReadLine();
               }
               carritoCliente.Estado = false;
               carritoService.UpdateCarrito(carritoCliente);
          }

          private static void MostrarCarrito(IClienteService clienteService, ICarritoService carritoService)
          {
               Console.Clear();
               Console.WriteLine("Lista de Clientes  : ");
               List<Cliente> client = clienteService.ShowClientes();

               foreach (var pr in client)
               {
                    Console.WriteLine(
                        "{0} ) || nombre: {1} || apellido: {2} ",
                        pr.ClienteId,
                        pr.Nombre,
                        pr.Apellido
                    );
                    Console.WriteLine("");
               }
               int select = InputValidations.IntInput("Ingresar : ", 1, client.Count);
               Cliente carrito = clienteService.GetClienteById(select);
               Carrito carro = carritoService.GetCarritoByClientId(carrito.ClienteId);
               if (carro != null)
               {
                    if (carro.Estado == true)
                    {
                         if (carro.CarritoProductos.Count != 0)
                         {
                              Console.WriteLine("MOSTRANDO CARRITO");
                              int count = 1;
                              foreach (var pr in carro.CarritoProductos)
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

               }
               else
               {
                    Console.WriteLine("El carrito está vacio!");
               }
          }

          public static void MostrarVentasPorDia(IVentasService ventasService)
          {
               Console.Clear();
               var ventas = ventasService.GetVentasByDay();
               int count = 1;
               foreach (var venta in ventas)
               {
                    Console.WriteLine(
                        "{0} ) || Fecha: {1} || Total: {2} ",
                        count,
                        venta.Fecha,
                        venta.Total
                    );
                    Console.WriteLine("");
                    count++;
               }
          }

          public static void MostrarVentasPorProducto(
              IVentasService ventasService,
              IProductService productService
          )
          {
               Console.Clear();
               var products = productService.ShowProducts();
               foreach (var pr in products)
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
               var ventas = ventasService.GetVentasByProduct(codigo);
               if (ventas.Count > 0)
               {
                    int count = 1;
                    foreach (var venta in ventas)
                    {
                         Console.WriteLine(
                             "{0} ) || Fecha: {1} || Total: {2} ||",
                             count,
                             venta.Fecha,
                             venta.Total
                         );
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
