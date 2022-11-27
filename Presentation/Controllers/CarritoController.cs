using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.model;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/carrito")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoService _carritoService;
        private readonly IProductService _productService;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public CarritoController(ICarritoService carritoService, IProductService productoService, IClienteService clienteService, IMapper mapper)
        {
            _carritoService = carritoService;
            _productService = productoService;
            _clienteService = clienteService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(CarritoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getCarritoById(int id)
        {
            try
            {
                var carrito =_carritoService.GetCarritoByClientId(id);
               
                    var carritoMap = _mapper.Map<CarritoDto>(carrito);
                   
                if (carritoMap != null)
                {
                    return Ok(carritoMap);
                }

                return NotFound();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Internal Server Error"+ e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
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
                        statuscode = "404"

                    };
                    return NotFound(error);

                }
                var product = _productService.FindRawProductById(agregadoProducto.ProductoId);
                if (product.Result == null)
                {
                    var error = new ErrorDto
                    {
                        message = "El Id del producto ingresado no existe.",
                        statuscode = "404"

                    };
                    return NotFound(error);

                }
                if (agregadoProducto.cantidad < 1)
                {
                    var error = new ErrorDto
                    {
                        message = "No se puede ingresar cantidad negativa o menos de 1",
                        statuscode = "404"

                    };
                    return NotFound(error);

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
                    var checkcarrito = _carritoService.GetCarritoProductoById(carrito.CarritoId, agregadoProducto.ProductoId);
                    if (checkcarrito != null) {
                        checkcarrito.Cantidad = checkcarrito.Cantidad + producto.Cantidad;                     
                        _carritoService.UpdateCarritoProducto(checkcarrito);
                        return NoContent();
                    }
                        carrito.CarritoProductos.Add(producto);
                    await _carritoService.UpdateCarrito(carrito);
                    return NoContent();
                }
            }
            catch (Exception e)
            {

                return BadRequest("se ha ingresado los datos en un formato incorrecto:"+e.Message);
            }


        }

        [HttpPatch]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ModifyProduct([FromBody] CompraCarritoDto agregadoProducto)
        {
            try
            {
                var client = _clienteService.GetClienteById(agregadoProducto.ClienteId);
                if (client == null)
                {
                    var error = new ErrorDto
                    {
                        message = "El Id del cliente ingresado no existe.",
                        statuscode = "404"

                    };
                    return NotFound(error);

                }
                var product = _productService.FindRawProductById(agregadoProducto.ProductoId);
                if (product.Result == null)
                {
                    var error = new ErrorDto
                    {
                        message = "El Id del producto ingresado no existe.",
                        statuscode = "404"

                    };
                    return NotFound(error);

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
                    var error = new ErrorDto
                    {
                        message = "El carrito esta vacio, porfavor ingrese productos.",
                        statuscode = "409"

                    };
                    return Conflict(error);

                }
                else
                {
                    var producto = _carritoService.GetCarritoProductoById(carrito.CarritoId, agregadoProducto.ProductoId);
                    if (producto == null)
                    {
                        var error = new ErrorDto
                        {
                            message = "Ese Producto no esta ingresado en el carrito, ingrese el Id de un producto ya existente dentro del carrito.",
                            statuscode = "409"

                        };
                        return Conflict(error);
                    }
                    producto.Cantidad = agregadoProducto.cantidad;
                    await _carritoService.UpdateCarritoProducto(producto);
                    return NoContent();
                }
            }
            catch (Exception)
            {

                return BadRequest("se ha ingresado los datos en un formato incorrecto");
            }


        }

        [HttpDelete("{ClientId}/{ProductoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProducto(int ClientId, int ProductoId)
        {
            try
            {
                var client = _clienteService.GetClienteById(ClientId);
                if (client == null)
                {
                    var error = new ErrorDto
                    {
                        message = "El Id del cliente ingresado no existe.",
                        statuscode = "404"

                    };
                    return NotFound(error);

                }
                var product = _productService.FindProductById(ProductoId);
                if (product == null)
                {
                    var error = new ErrorDto
                    {
                        message = "El Id del producto ingresado no existe.",
                        statuscode = "404"

                    };
                    return NotFound(error);

                }
                var carrito = _carritoService.GetCarritoByClientId(ClientId);
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
                    var producto = _carritoService.GetCarritoProductoById(carrito.CarritoId, ProductoId);
                    if (producto == null)
                    {
                        var error = new ErrorDto
                        {
                            message = "Ese Producto no esta ingresado en el carrito o ya fue eliminado., ingrese el Id de un producto ya existente dentro del carrito.",
                            statuscode = "404"

                        };
                        return NotFound(error);
                    }
                    await _carritoService.DeleteCarritoProducto(producto);
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
