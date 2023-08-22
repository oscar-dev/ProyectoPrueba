using System;
using System.Collections.Generic;

namespace CoderPadTest.Models;

public partial class Movimiento
{
    public int MovimientoId { get; set; }

    public int CuentaId { get; set; }

    public DateTime Fecha { get; set; }

    public int TipoId { get; set; }

    public decimal Valor { get; set; }

    public decimal Saldo { get; set; }
}
