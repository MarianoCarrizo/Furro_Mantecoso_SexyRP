using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
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

                if(cliente != null)
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
    }
}
