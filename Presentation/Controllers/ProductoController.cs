using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
     [Route("api/productos")]
     [ApiController]
     public class ProductoController : ControllerBase
     {
          private readonly IProductService _productService;

          public ProductoController(IProductService productService)
          {
               _productService = productService;
          }

          [HttpGet]
          [ProducesResponseType(typeof(List<ProductoDto>), StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
          public async Task<IActionResult> GetProductos([FromQuery] string? name = null, [FromQuery] bool? sort = null)
          {
               try
               {
                    var productos = await _productService.GetProducts(name, sort);
                    return Ok(productos);

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