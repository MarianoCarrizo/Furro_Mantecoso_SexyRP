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


          [HttpPost]
          [ProducesResponseType(typeof(Carrito), StatusCodes.Status201Created)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
          public async Task<IActionResult> AddCarrito([FromBody] Carrito carrito)
          {
               try
               {
                    var cliente =  _carritoService.CreateCarrito(carrito);

                    if (cliente == null)
                    {
                         var errorDto = new ErrorDto
                         {
                              message = "Ha ocurrido un error. El DNI ingresado corresponde a un usuario ya existente en el sistema.",
                              statuscode = "409"
                         };
                         return Conflict(errorDto);
                    }
                    return Created("Se ha creado nuevo Cliente", carrito);

               }
               catch (Exception)
               {

                    return BadRequest("se ha ingresado los datos en un formato incorrecto");
               }


          }
     }
}
