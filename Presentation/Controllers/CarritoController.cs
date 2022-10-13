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

          public CarritoController(ICarritoService carritoService)
          {
               _carritoService = carritoService;
          }

          
          [HttpPut]
          [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status201Created)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
          public async Task<IActionResult> AddCarritoProducto([FromBody] CompraCarritoDto agregadoProducto)
          {
               try
               {
                   
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
                         var responseDto = new ResponseDto
                         {
                              message = "Se ha insertado el producto Correctamente.",
                              statuscode = "201",
                         };
                         return new JsonResult(responseDto) { StatusCode = 201 };
                    }
               }
               catch (Exception)
               {

                    return BadRequest("se ha ingresado los datos en un formato incorrecto");
               }


          }
     }
}
