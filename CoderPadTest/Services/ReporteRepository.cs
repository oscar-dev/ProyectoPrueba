using CoderPadTest.DTOs;
using CoderPadTest.Interfaces;
using CoderPadTest.Models;

namespace CoderPadTest.Services
{
    public class ReporteRepository
            : IReporteRepository, IDisposable
    {
        private BaseContext _baseContext;
        private bool disposed = false;

        public ReporteRepository(BaseContext baseContext)
        {
            _baseContext = baseContext;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._baseContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public IList<RptMovimientoDTO> ConsultarMovimientos(DateTime fechaDesde, DateTime fechaHasta, int clienteId)
        {
            DateTime initDate = new DateTime(fechaDesde.Year, fechaDesde.Month, fechaDesde.Day);
            DateTime endDate = new DateTime(fechaHasta.Year, fechaHasta.Month, fechaHasta.Day, 23, 59, 59);

            using (var context = this._baseContext)
            {
                var movimientos = from m in context.Movimientos
                                  join tm in context.TiposMovimiento on m.TipoId equals tm.TipoMovimientoId
                                  join c in context.Cuentas on m.CuentaId equals c.CuentaId
                                  join tc in context.TiposCuenta on c.TipoCuentaId equals tc.TipoCuentaId 
                                  join cli in context.Clientes on c.ClienteId equals cli.ClienteId
                                  join p in context.Personas on cli.PersonaId equals p.PersonaId
                                  where m.Fecha >= initDate && m.Fecha <= endDate 
                                    && ( c.ClienteId == clienteId || clienteId == -1 )
                              select new RptMovimientoDTO
                              {
                                  Cliente = p.Nombre,
                                  Cuenta = c.NroCuenta,
                                  Fecha = m.Fecha,
                                  Movimiento = m.Valor,
                                  SaldoDisponible = c.Saldo,
                                  Tipo = tm.Descripcion,
                                  Estado = c.Estado,
                                  SaldoInicial = c.SaldoInicial
                              };

                return movimientos.ToList();
            }
        }
    }
}
