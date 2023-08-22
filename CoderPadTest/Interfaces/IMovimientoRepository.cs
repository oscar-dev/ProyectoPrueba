using CoderPadTest.DTOs;

namespace CoderPadTest.Interfaces
{
    public interface IMovimientoRepository
    {
        void insertMovimiento(MovimientoDTO movimientoDTO);
        void updateMovimiento(MovimientoDTO movimientoDTO);
        void deleteMovimiento(int movimientoId);
        IList<MovimientoDTO> getMovimientos();
        IList<TipoMovimientoDTO> getTiposMovimiento();
        MovimientoDTO getMovimientoById(int movimientoId);
    }
}
