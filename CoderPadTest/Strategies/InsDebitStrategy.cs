using CoderPadTest.DTOs;
using CoderPadTest.Exceptions;
using CoderPadTest.Interfaces;
using CoderPadTest.Models;

namespace CoderPadTest.Strategies
{
    public class InsDebitStrategy : MovimientoStrategy, IMovimientoStrategy
    {
        public InsDebitStrategy(BaseContext baseContext, MovimientoDTO movimientoDTO)
            : base(baseContext, movimientoDTO)
        {
        }
        public override void Execute()
        {
            var cuenta = getCuenta();

            if( (cuenta.Saldo - this._movimientoDTO.Valor) <= 0 )
            {
                throw new BalanceNotAvailException("Saldo no disponible");
            }

            var debitosDelDia = this.getDebitosDiarios();

            if( (this._movimientoDTO.Valor+debitosDelDia) > cuenta.LimiteDiario )
            {
                throw new DailyLimitExException("Cupo diario Excedido");
            }

            cuenta.Saldo -= this._movimientoDTO.Valor;

            this._movimientoDTO.Saldo = cuenta.Saldo;

            this._movimientoDTO.Valor *= -1;
            
            ActualizarCuenta(cuenta);

            InsertarMovimiento();
        }
    }
}
