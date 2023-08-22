using CoderPadTest.DTOs;

namespace CoderPadTest.Interfaces
{
    public interface IReporteRepository
    {
        IList<RptMovimientoDTO> ConsultarMovimientos(DateTime fechaDesde, DateTime fechaHasta, int clienteId);
    }
}
