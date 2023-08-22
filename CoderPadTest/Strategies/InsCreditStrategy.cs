using CoderPadTest.DTOs;
using CoderPadTest.Interfaces;
using CoderPadTest.Models;

namespace CoderPadTest.Strategies
{
    public class InsCreditStrategy : MovimientoStrategy, IMovimientoStrategy
    {
        public InsCreditStrategy(BaseContext baseContext, MovimientoDTO movimientoDTO)
            : base(baseContext, movimientoDTO)
        {
        }
        public override void Execute()
        {
            var cuenta = getCuenta();

            cuenta.Saldo += this._movimientoDTO.Valor;

            this._movimientoDTO.Saldo = cuenta.Saldo;

            ActualizarCuenta(cuenta);

            InsertarMovimiento();
        }
    }
}
