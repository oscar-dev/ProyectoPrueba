using CoderPadTest.DTOs;
using CoderPadTest.Exceptions;
using CoderPadTest.Interfaces;
using CoderPadTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoderPadTest.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;
        private readonly ILogger _logger;
        public ClienteController(ILogger<ClienteController> logger, IClienteRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult index()
        {
            try
            {
                this._logger.LogInformation("Listado de clientes");

                IList<ClienteDTO> list = this._repository.getClientes();

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", clientes = list });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error listando clientes. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult insert(ClienteDTO cliente)
        {
            try
            {
                this._logger.LogInformation("Insertando cliente");

                this._repository.insertCliente(cliente);

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", message = "Cliente creado correctamente"});
            }
            catch (Exception e)
            {
                this._logger.LogError("Error tratando de insertar cliente. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
         }
        [HttpPut]
        public IActionResult update(ClienteDTO cliente)
        {
            try
            {
                this._logger.LogInformation("Actualizando cliente");

                this._repository.updateCliente(cliente);

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", message = "Cliente actualizado correctamente" });
            }
            catch (NotFoundException nfe)
            {
                this._logger.LogWarning("No se encontro el cliente para actualizar. Error: " + nfe.Message);

                return StatusCode(StatusCodes.Status404NotFound, new { result = "ERROR", message = nfe.Message });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error actualizando cliente. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult delete(int id)
        {
            try
            {
                this._logger.LogInformation("Borrando cliente");

                this._repository.deleteCliente(id);

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", message = "Cliente eliminado correctamente" });
            }
            catch(NotFoundException nfe)
            {
                this._logger.LogWarning("No se encontro el cliente para borrar. Error: " + nfe.Message);

                return StatusCode(StatusCodes.Status404NotFound, new { result = "ERROR", message = nfe.Message });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error borrando cliente. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }
    }
}
