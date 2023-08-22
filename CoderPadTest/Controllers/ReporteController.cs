using CoderPadTest.DTOs;
using CoderPadTest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoderPadTest.Controllers
{
    [Route("api/reportes")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteRepository _repository;
        public ReporteController(IReporteRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult index(DateTime fechaDesde, DateTime fechaHasta, int? clienteId)
        {
            try
            {
                int cliente = (int)(clienteId == null ? -1 : clienteId);

                IList<RptMovimientoDTO> list = this._repository.ConsultarMovimientos(fechaDesde, fechaHasta, cliente);

                return StatusCode(StatusCodes.Status200OK, new { result = "OK", registros = list });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "ERROR", message = e.Message });
            }
        }
    }
}
