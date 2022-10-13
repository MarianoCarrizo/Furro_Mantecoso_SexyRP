using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClienteById(int id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteById(id);

                if (cliente != null)
                {
                    return Ok(cliente);
                }

                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCliente([FromBody] ClienteDto client)
        {
            try
            {
                var cliente = await _clienteService.CreateClient(client);

                if (cliente == null)
                {
                    var errorDto = new ErrorDto
                    {
                        message = "Ha ocurrido un error. El DNI ingresado corresponde a un usuario ya existente en el sistema.",
                        statuscode = "409"
                    };
                    return Conflict(errorDto);
                }
                return Created("Se ha creado nuevo Cliente", client);

            }
            catch (Exception)
            {

                return BadRequest("se ha ingresado los datos en un formato incorrecto");
            }
        }
    }
}
