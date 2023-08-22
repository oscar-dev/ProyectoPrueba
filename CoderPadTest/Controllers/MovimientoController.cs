using CoderPadTest.DTOs;
using CoderPadTest.Exceptions;
using CoderPadTest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoderPadTest.Controllers
{
    [Route("api/movimientos")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoRepository _repository;
        private readonly ILogger _logger;
        public MovimientoController(ILogger<MovimientoController> logger, IMovimientoRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult index()
        {
            try
            {
                this._logger.LogInformation("Listado de movimientos");

                IList<MovimientoDTO> list = this._repository.getMovimientos();

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", movimientos = list });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error listando movimientos. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult insert(MovimientoDTO movimientoDTO)
        {
            try
            {
                this._logger.LogInformation("Insertando movimientos");

                this._repository.insertMovimiento(movimientoDTO);

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", message = "Movimiento creado correctamente" });
            }
            catch (NotFoundException nfe)
            {
                this._logger.LogError("No se encontraron datos relacionados al movimiento. Error: " + nfe.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = nfe.Message });

            }
            catch (BalanceNotAvailException b)
            {
                this._logger.LogWarning("Se intenta insertar un debito pero no tiene saldo suficiente. Error: " + b.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = b.Message });

            }
            catch (DailyLimitExException d)
            {
                this._logger.LogWarning("Limite diario excedido. Error: " + d.Message);

                return StatusCode(StatusCodes.Status404NotFound, new { result = "ERROR", message = d.Message });
            }
            catch(Exceptions.NotImplementedException nie)
            {
                this._logger.LogError("Se detecto un tipo de movimiento no implementado. Error: " + nie.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = nie.Message });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error tratando de insertar movimiento. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }
        [HttpDelete]
        public IActionResult delete(int id)
        {
            try
            {
                this._logger.LogInformation("Borrando movimiento");

                this._repository.deleteMovimiento(id);

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", message = "Movimiento eliminado correctamente" });
            }
            catch (NotFoundException nfe)
            {
                this._logger.LogWarning("No se encontro el movimiento a borrar. Error: " + nfe.Message);

                return StatusCode(StatusCodes.Status404NotFound, new { result = "ERROR", message = nfe.Message });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error borrando movimiento. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }

        [Route("TiposMovimientos")]
        [HttpGet]
        public IActionResult getTiposMovimientos()
        {
            try
            {
                this._logger.LogInformation("Listando tipos de movimientos");

                IList<TipoMovimientoDTO> list = this._repository.getTiposMovimiento();

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", tipos_movimientos = list });
            }
            catch (Exception e)
            {
                this._logger.LogError("Error listando tipos de movimientos. Error: " + e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }
    }
}
