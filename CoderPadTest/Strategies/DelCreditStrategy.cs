using CoderPadTest.DTOs;
using CoderPadTest.Interfaces;
using CoderPadTest.Models;

namespace CoderPadTest.Strategies
{
    public class DelCreditStrategy : MovimientoStrategy, IMovimientoStrategy
    {
        public DelCreditStrategy(BaseContext baseContext, MovimientoDTO movimientoDTO) 
            : base(baseContext, movimientoDTO)
        {
        }
        public override void Execute()
        {
            var cuenta = getCuenta();

            cuenta.Saldo -= this._movimientoDTO.Valor;

            ActualizarCuenta(cuenta);

            BorrarMovimiento();
        }
    }
}
