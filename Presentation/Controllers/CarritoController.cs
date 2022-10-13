using Application.Services;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
     [Route("api/carrito")]
     [ApiController]
     public class CarritoController : ControllerBase
     {
          private readonly ICarritoService _carritoService;
          private readonly IProductService _productService;
          private readonly IClienteService _clienteService;

          public CarritoController(ICarritoService carritoService,IProductService productoService, IClienteService clienteService)
          {
               _carritoService = carritoService;
               _productService = productoService;
               _clienteService = clienteService;
          }

          
          [HttpPut]
          [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status204NoContent)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
          public async Task<IActionResult> AddProducto([FromBody] CompraCarritoDto agregadoProducto)
          {
               try
               {
                    var client = _clienteService.GetClienteById(agregadoProducto.ClienteId);
                    if (client == null)
                    {
                         var error = new ErrorDto
                         {
                              message = "El Id del cliente ingresado no existe.",
                              statuscode = "409"

                         };
                         return Conflict(error);

                    }
                    var producto = _productService.FindProductById(agregadoProducto.ProductoId);
                    if (producto == null)
                    {
                         var error = new ErrorDto
                         {
                              message = "El Id del producto ingresado no existe.",
                              statuscode = "409"

                         };
                         return Conflict(error);

                    }
                    if (agregadoProducto.cantidad < 1)
                    {
                         var error = new ErrorDto
                         {
                              message = "No se puede ingresar cantidad negativa o menos de 1",
                              statuscode = "409"

                         };
                         return Conflict(error);

                    }

                    var carrito = _carritoService.GetCarritoByClientId(agregadoProducto.ClienteId);
                    if (carrito == null)
                    {
                         var carro = new Carrito
                         {
                              CarritoId = Guid.NewGuid(),
                              ClienteId = agregadoProducto.ClienteId,
                              Estado = true,
                         };
                         var producto = new CarritoProducto
                         {
                              Cantidad = agregadoProducto.cantidad,
                              ProductoId = agregadoProducto.ProductoId,
                         };
                         carro.CarritoProductos.Add(producto);
                         var carroCreado = await _carritoService.CreateCarrito(carro);
                         return NoContent();

                    }
                    else
                    {
                         var producto = new CarritoProducto
                         {
                              Cantidad = agregadoProducto.cantidad,
                              ProductoId = agregadoProducto.ProductoId,
                         };
                         carrito.CarritoProductos.Add(producto);
                          await _carritoService.UpdateCarrito(carrito);
                         return NoContent();
                    }
               }
               catch (Exception)
               {

                    return BadRequest("se ha ingresado los datos en un formato incorrecto");
               }


          }
     }
}
