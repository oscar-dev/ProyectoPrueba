using CoderPadTest.DTOs;
using CoderPadTest.Exceptions;
using CoderPadTest.Interfaces;
using CoderPadTest.Models;

namespace CoderPadTest.Strategies
{
    public class DelDebitStrategy : MovimientoStrategy, IMovimientoStrategy 
    {
        public DelDebitStrategy(BaseContext baseContext, MovimientoDTO movimientoDTO)
            : base(baseContext, movimientoDTO)
        {
        }
        public override void Execute()
        {
            var cuenta = getCuenta();

            cuenta.Saldo += (this._movimientoDTO.Valor*-1);

            ActualizarCuenta(cuenta);

            BorrarMovimiento();
        }
    }
}
