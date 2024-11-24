using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.model;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    /// <summary>
    /// Controller for carrito operations. 
    /// </summary>
    [Route("api/carrito")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoService _carritoService;
        private readonly IProductService _productService;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for CarritoController
        /// </summary>
        /// <param name="carritoService">An instance of CarritoService.</param>
        /// <param name="productoService">An instance of ProductoService</param>
        /// <param name="clienteService">An instance of ClienteService</param>
        /// <param name="mapper">An instance of the mapping profile from AutoMapper.</param>
        public CarritoController(ICarritoService carritoService, IProductService productoService, IClienteService clienteService, IMapper mapper)
        {
            //Notes from Konta: The descriptions in the summary are deliveratelly vague. The idea is showing you that you are expected to document these parameters and give them a meaningful description.
            _carritoService = carritoService;
            _productService = productoService;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        /// <summary>
        /// This is an action method to obtain an existing carrito given a valid id. If the provided id does not match with an existing carrito, a NotFound response will be returned.
        /// </summary>
        /// <param name="id">The id of the carrito.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CarritoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCarritoById(int id)
        {
            var carrito = await _carritoService.GetCarritoByClientId(id);
            var carritoMap = _mapper.Map<CarritoDto>(carrito);

            if (carritoMap != null)
            {
                return Ok(carritoMap);
            }

            return NotFound("No se encontró un carrito con el Id provisto.");
        }

        /// <summary>
        /// Action method to add a product to a carrito.
        /// </summary>
        /// <param name="agregadoProducto">Product to be added.</param>
        /// <returns>An http response.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProducto([FromBody] CompraCarritoDto agregadoProducto)
        {
            try
            {
                var client = await _clienteService.GetClienteById(agregadoProducto.ClienteId);

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

                var carrito = await _carritoService.GetCarritoByClientId(agregadoProducto.ClienteId);
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
                    var checkcarrito = await _carritoService.GetCarritoProductoById(carrito.CarritoId, agregadoProducto.ProductoId);
                    if (checkcarrito != null)
                    {
                        checkcarrito.Cantidad = checkcarrito.Cantidad + producto.Cantidad;
                        await _carritoService.UpdateCarritoProducto(checkcarrito);
                        return NoContent();
                    }
                    carrito.CarritoProductos.Add(producto);
                    await _carritoService.UpdateCarrito(carrito);
                    return NoContent();
                }
            }
            catch (Exception e)
            {

                return BadRequest("se ha ingresado los datos en un formato incorrecto:" + e.Message);
            }


        }

        /// <summary>
        /// Action method to modify a product from a carrito.
        /// </summary>
        /// <param name="agregadoProducto">Product to be added.</param>
        /// <returns>A corresponding http response.</returns>
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

                var carrito = await _carritoService.GetCarritoByClientId(agregadoProducto.ClienteId);
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
                    var producto = await _carritoService.GetCarritoProductoById(carrito.CarritoId, agregadoProducto.ProductoId);
                    if (producto == null)
                    {
                        var error = new ErrorDto
                        {
                            message = "Ese Producto no esta ingresado en el carrito, ingrese el Id de un producto ya existente dentro del carrito.",
                            statuscode = "409"

                        };
                        return Conflict(error);
                    }
                    if (producto.Cantidad == 0)
                    {
                        var error = new ErrorDto
                        {
                            message = "No se puede tener menos de un producto en el carrito.",
                            statuscode = "409"

                        };
                        return Conflict(error);
                    }
                    producto.Cantidad += agregadoProducto.cantidad;
                    await _carritoService.UpdateCarritoProducto(producto);
                    return NoContent();
                }
            }
            catch (Exception)
            {

                return BadRequest("se ha ingresado los datos en un formato incorrecto");
            }


        }

        /// <summary>
        /// Action method to delete a product from a carrito.
        /// </summary>
        /// <param name="ClientId">The client Id</param>
        /// <param name="ProductoId">The product Id.</param>
        /// <returns>An http response.</returns>
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
                var carrito = await _carritoService.GetCarritoByClientId(ClientId);
                if (carrito == null)
                {
                    var error = new ErrorDto
                    {
                        message = "El carrito esta vacio, porfavor ingrese productos.",
                        statuscode = "409"

                    };
                    return NoContent();

                }
                else
                {
                    var producto = await _carritoService.GetCarritoProductoById(carrito.CarritoId, ProductoId);
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

        /// <summary>
        /// An action method to delete an existing carrito.
        /// </summary>
        /// <param name="carritoId">The id of the carrito to be deleted.</param>
        /// <returns>An http response.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCarrito([FromQuery] Guid carritoId)
        {
            try
            {
                var carrito = await _carritoService.DeleteCarrito(carritoId);
                return NoContent();
            }

            catch (FileNotFoundException e)
            {
                var error = new ErrorDto
                {
                    message = e.Message,
                    statuscode = "404"
                };
                return NotFound(error);

            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }


        }
    }
}
