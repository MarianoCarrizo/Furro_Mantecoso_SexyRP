﻿using Application.Services;
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

          [HttpGet("{id}")]
          [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(ErrorDto),StatusCodes.Status404NotFound)]
          [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
          public async Task<IActionResult> GetProduct(int id)
          {
               try
               {
                    var cliente = await _productService.FindProductById(id);

                    if (cliente != null)
                    {
                         return Ok(cliente);
                    }
                    var error = new ErrorDto()
                    {
                         message = "Producto no encontrado",
                         statuscode = "404",
                    };
                    return NotFound(error);
               }
               catch (Exception)
               {

                    return StatusCode(500, "Internal Server Error");
               }
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