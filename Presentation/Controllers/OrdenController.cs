using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
     [Route("api/Orden")]
     [ApiController]
     public class OrdenController : ControllerBase
     {
          private readonly IOrdenService _ordenService;
          private readonly ICarritoService _carritoService;
          private readonly IClienteService _clienteService;
          private readonly IMapper _mapper;

          public OrdenController(ICarritoService carritoService, IOrdenService ordenService, IClienteService clienteService, IMapper mapper)
          {
               _carritoService = carritoService;
               _ordenService = ordenService;
               _clienteService = clienteService;
               _mapper = mapper;
          }


          [HttpPost("{clientId}")]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
          public async Task<IActionResult> AddOrden(int clientId)
          {
               try
               {
                    var client = _clienteService.GetClienteById(clientId);
                    if (client == null)
                    {
                         var error = new ErrorDto
                         {
                              message = "El Id del cliente ingresado no existe.",
                              statuscode = "404"

                         };
                         return NotFound(error);

                    }

                    var carrito = _carritoService.GetCarritoByClientId(clientId);
                    if (carrito == null)
                    {
                         var error = new ErrorDto
                         {
                              message = "El carrito esta vacio, porfavor ingrese productos.",
                              statuscode = "409"

                         };
                         return Conflict(error);

                    }
                    else
                    {
                         decimal total = 0;
                         foreach (CarritoProducto producto in carrito.CarritoProductos)
                         {
                              total += producto.Producto.Precio * producto.Cantidad;

                         }
                         var Orden = new Orden
                         {
                              OrdenId = Guid.NewGuid(),
                              CarritoId = carrito.CarritoId,
                              Total = total,
                              Fecha = DateTime.Now

                         };
                         _ordenService.CreateOrden(Orden);
                         return Ok("Se ha creado una Orden");
                    }
               }
               catch (Exception)
               {

                    return BadRequest("se ha ingresado los datos en un formato incorrecto");
               }


          }


          [HttpGet]
          [ProducesResponseType(typeof(List<OrdenDto>), StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
          public async Task<IActionResult> GetOrdenes([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
          {
               try
               {
                    var order = await _ordenService.GetOrder(from, to);


                    List<OrdenDto> ordenes = new List<OrdenDto>();
                    foreach (var item in order)
                    {
                         List<ProductoDto> productos = new List<ProductoDto>();
                         var carrito = _carritoService.GetCarritoById(item.CarritoId);
                         foreach (var product in carrito.CarritoProductos)
                         {
                              productos.Add(_mapper.Map<ProductoDto>(product.Producto));
                         }
                         var ordenMapeada = _mapper.Map<OrdenDto>(item);
                         ordenMapeada.Productos = productos;
                         ordenes.Add(ordenMapeada);
                    }

                    return Ok(ordenes);

               }
               catch (Exception e)
               {
                    var error = new ErrorDto()
                    {
                         message = e.Message,
                         statuscode = "404",
                    };
                    return NotFound(error);
               }


          }


     }
}
