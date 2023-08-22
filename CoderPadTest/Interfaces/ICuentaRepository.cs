using CoderPadTest.DTOs;

namespace CoderPadTest.Interfaces
{
    public interface ICuentaRepository
    {
        void insertCuenta(CuentaDTO cuentaDTO);
        void updateCuenta(CuentaDTO cuentaDTO);
        void deleteCuenta(int cuentaId);
        IList<TipoCuentaDTO> getTiposCuenta();
        IList<CuentaDTO> getCuentas();
        CuentaDTO getCuentaById(int cuentaId);
    }
}
