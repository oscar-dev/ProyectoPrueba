using System;
using System.Collections.Generic;

namespace CoderPadTest.Models;

public partial class Cuenta
{
    public int CuentaId { get; set; }

    public int ClienteId { get; set; }

    public string NroCuenta { get; set; } = null!;

    public int TipoCuentaId { get; set; }

    public decimal SaldoInicial { get; set; }

    public decimal Saldo { get; set; }

    public decimal LimiteDiario { get; set; }

    public bool Estado { get; set; }
}
