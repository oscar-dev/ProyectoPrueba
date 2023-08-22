using CoderPadTest.DTOs;
using CoderPadTest.Exceptions;
using CoderPadTest.Interfaces;
using CoderPadTest.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Filters;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoderPadTest.Strategies
{
    abstract public class MovimientoStrategy : IMovimientoStrategy
    {
        protected BaseContext _baseContext;
        protected MovimientoDTO _movimientoDTO;
        
        public MovimientoStrategy(BaseContext baseContext, MovimientoDTO movimientoDTO)
        {
            _baseContext = baseContext;
            _movimientoDTO = movimientoDTO;
        }
        protected Cuenta getCuenta()
        {
            Cuenta cuenta;

            cuenta = this._baseContext.Cuentas.FirstOrDefault(c => c.CuentaId == this._movimientoDTO.CuentaId);

            if (cuenta == null)
            {
                throw new NotFoundException("Cuenta no encontrada. Id: " + this._movimientoDTO.CuentaId);
            }

            return cuenta;
        }
        protected decimal getDebitosDiarios()
        {
            DateTime initDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime endDate = new DateTime(DateTime.Now.Year, 
                                            DateTime.Now.Month,
                                            DateTime.Now.Day,
                                            23, 59, 59);
            
            var totales = this._baseContext.Movimientos
                        .Where( x => x.Fecha >= initDate && x.Fecha <= endDate
                                && x.CuentaId == this._movimientoDTO.CuentaId 
                                && x.TipoId == 1 )  // Tipo de Movimiento debito
                        .Sum( s => (s.Valor * -1) );

            return totales;
        }
        protected void InsertarMovimiento()
        {
            var movimiento = new Movimiento()
            {
                CuentaId = this._movimientoDTO.CuentaId,
                Fecha = this._movimientoDTO.Fecha,
                Saldo = this._movimientoDTO.Saldo,
                TipoId = this._movimientoDTO.TipoId,
                Valor = this._movimientoDTO.Valor
            };

            this._baseContext.Movimientos.Add(movimiento);

            this._baseContext.SaveChanges();
        }
        protected void BorrarMovimiento()
        {
            var movimiento = this._baseContext.Movimientos.Where(c => c.MovimientoId == this._movimientoDTO.MovimientoId).FirstOrDefault();

            if (movimiento != null)
            {
                this._baseContext.Movimientos.Remove(movimiento);
                this._baseContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException("Movimiento no encontrado. Id: " + this._movimientoDTO.MovimientoId);
            }
        }
        protected void ActualizarCuenta(Cuenta cuenta)
        {
            var cuentaEntry = this._baseContext.Entry(cuenta);

            cuentaEntry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            this._baseContext.SaveChanges();
        }
        
        abstract public void Execute();
    }
}
