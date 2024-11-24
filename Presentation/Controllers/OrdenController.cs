using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

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
                    carrito.Estado = false;
                    await _carritoService.UpdateCarrito(carrito);
                    return Ok("Se ha creado una Orden");
                }
            }
            catch (Exception)
            {

                return BadRequest("se ha ingresado los datos en un formato incorrecto");
            }


        }


        [HttpGet]
        [ProducesResponseType(typeof(OrdenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrdenes(int limit = 5, int page = 1, DateTime? from = null, DateTime? to = null)
        {
            try
            {
                var order = await _ordenService.GetOrder(limit, page, from, to);
                return Ok(order);
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
