using CoderPadTest.DTOs;
using CoderPadTest.Exceptions;
using CoderPadTest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoderPadTest.Controllers
{
    [Route("api/cuentas")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaRepository _repository;
        private readonly ILogger _logger;

        public CuentaController(ILogger<CuentaController> logger, ICuentaRepository repository)
        {
            this._repository = repository;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult index()
        {
            try
            {
                this._logger.LogInformation("Listado de cuentas");

                IList<CuentaDTO> list = this._repository.getCuentas();

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", cuentas = list });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error listando cuentas. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }
        
        [HttpPost]
        public IActionResult insert(CuentaDTO cuenta)
        {
            try
            {
                this._logger.LogInformation("Insertando cuenta");

                this._repository.insertCuenta(cuenta);

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", message = "Cuenta creada correctamente" });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error tratando de insertar cuenta. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }
        [HttpPut]
        public IActionResult update(CuentaDTO cuenta)
        {
            try
            {
                this._logger.LogInformation("Actualizando cuenta");

                this._repository.updateCuenta(cuenta);

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", message = "Cuenta actualizada correctamente" });
            }
            catch (NotFoundException nfe)
            {
                this._logger.LogWarning("No se encontraron datos relacionados. Error: " + nfe.Message);

                return StatusCode(StatusCodes.Status404NotFound, new { result = "ERROR", message = nfe.Message });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error actualizando cuenta. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult delete(int id)
        {
            try
            {
                this._logger.LogInformation("Borrando cuenta");

                this._repository.deleteCuenta(id);

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", message = "Cuenta eliminada correctamente" });
            }
            catch (NotFoundException nfe)
            {
                this._logger.LogWarning("No se encontro la cuenta a borrar. Error: " + nfe.Message);

                return StatusCode(StatusCodes.Status404NotFound, new { result = "ERROR", message = nfe.Message });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error borrando cuenta. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }

        [Route("TiposCuentas")]
        [HttpGet]
        public IActionResult getTiposCuentas()
        {
            try
            {
                this._logger.LogInformation("Listando tipos de cuentas");

                IList<TipoCuentaDTO> list = this._repository.getTiposCuenta();

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", tipos_cuentas = list });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error listando tipos de cuentas. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }
    }
}
