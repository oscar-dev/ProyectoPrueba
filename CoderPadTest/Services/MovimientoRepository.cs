using CoderPadTest.DTOs;
using CoderPadTest.Exceptions;
using CoderPadTest.Interfaces;
using CoderPadTest.Models;
using CoderPadTest.Strategies;

namespace CoderPadTest.Services
{
    public class MovimientoRepository
        : IMovimientoRepository, IDisposable
    {
        private BaseContext _baseContext;
        private bool disposed = false;

        public MovimientoRepository(BaseContext baseContext)
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
        public void insertMovimiento(MovimientoDTO movimientoDTO)
        {
            validarMovimientoDTO(movimientoDTO);

            IMovimientoStrategy movimientoStrategy;

            switch( movimientoDTO.TipoId)
            {
                case 1:     // Debito
                    movimientoStrategy = new InsDebitStrategy(this._baseContext, movimientoDTO);
                    break;
                case 2:     // Credito
                    movimientoStrategy = new InsCreditStrategy(this._baseContext, movimientoDTO);
                    break;
                default:
                    throw new Exceptions.NotImplementedException( "Funcionalidad no implementada para el insert del tipo de movimiento: " + movimientoDTO.TipoId);
            }

            movimientoStrategy.Execute();
        }
        private bool validarMovimientoDTO(MovimientoDTO movimientoDTO)
        {
            if (movimientoDTO.CuentaId <= 0)
            {
                throw new Exception("Cuenta inválida");
            }

            var cuenta = this._baseContext.Cuentas.Where(c => c.CuentaId == movimientoDTO.CuentaId).FirstOrDefault();

            if (cuenta == null)
            {
                throw new NotFoundException("La cuenta no existe");
            }

            if (movimientoDTO.TipoId <= 0)
            {
                throw new Exception("Tipo de movimiento inválido");
            }

            var tipoMovimiento = this._baseContext.TiposMovimiento.Where(c => c.TipoMovimientoId == movimientoDTO.TipoId).FirstOrDefault();

            if (tipoMovimiento == null)
            {
                throw new NotFoundException("El tipo de movimiento no existe");
            }

            return true;
        }


        public void updateMovimiento(MovimientoDTO movimientoDTO)
        {
            validarMovimientoDTO(movimientoDTO);

            var movimiento = this._baseContext.Movimientos.Where(c => c.MovimientoId == movimientoDTO.MovimientoId).FirstOrDefault();

            if (movimiento != null)
            {
                movimiento.MovimientoId = movimientoDTO.MovimientoId;
                movimiento.TipoId = movimientoDTO.TipoId;
                movimiento.CuentaId = movimientoDTO.CuentaId;
                movimiento.Fecha = movimientoDTO.Fecha;
                movimiento.Saldo = movimientoDTO.Saldo;
                movimiento.Valor = movimientoDTO.Valor;

                this._baseContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException("Movimiento no encontrado. Id: " + movimientoDTO.MovimientoId);
            }
        }
        public void deleteMovimiento(int movimientoId)
        {
            MovimientoDTO movimientoDTO = getMovimientoById(movimientoId);

            IMovimientoStrategy movimientoStrategy;

            switch (movimientoDTO.TipoId)
            {
                case 1:     // Debito
                    movimientoStrategy = new DelDebitStrategy(this._baseContext, movimientoDTO);
                    break;
                case 2:     // Credito
                    movimientoStrategy = new DelCreditStrategy(this._baseContext, movimientoDTO);
                    break;
                default:
                    throw new Exceptions.NotImplementedException("Funcionalidad no implementada para el delete del tipo de movimiento: " + movimientoDTO.TipoId);
            }

            movimientoStrategy.Execute();
        }
        public IList<MovimientoDTO> getMovimientos()
        {            
            var movimientos = from m in this._baseContext.Movimientos
                                join t in this._baseContext.TiposMovimiento on m.TipoId equals t.TipoMovimientoId
                                select new MovimientoDTO
                                {
                                    MovimientoId = m.MovimientoId,
                                    TipoId = m.TipoId,
                                    TipoMovimiento = t.Descripcion,
                                    CuentaId = m.CuentaId,
                                    Fecha = m.Fecha,
                                    Saldo = m.Saldo,
                                    Valor = m.Valor
                                };

            return movimientos.ToList();
        }
        public MovimientoDTO getMovimientoById(int movimientoId)
        {
            var movimiento = (from m in this._baseContext.Movimientos
                                join t in this._baseContext.TiposMovimiento on m.TipoId equals t.TipoMovimientoId
                                where m.MovimientoId == movimientoId
                                select new MovimientoDTO
                                {
                                    MovimientoId = m.MovimientoId,
                                    TipoId = m.TipoId,
                                    TipoMovimiento = t.Descripcion,
                                    CuentaId = m.CuentaId,
                                    Fecha = m.Fecha,
                                    Saldo = m.Saldo,
                                    Valor = m.Valor
                                }).Take(1)
                                .FirstOrDefault();

            if (movimiento == null)
            {
                throw new NotFoundException("Movimiento no encontrado. Id: " + movimientoId);
            }

            return movimiento;
        }
        public IList<TipoMovimientoDTO> getTiposMovimiento()
        {
            var tiposMovimiento = from tc in this._baseContext.TiposMovimiento
                                select new TipoMovimientoDTO
                                {
                                    TipoMovimientoId = tc.TipoMovimientoId,
                                    Descripcion = tc.Descripcion,
                                };

            return tiposMovimiento.ToList();
        }
    }
}
