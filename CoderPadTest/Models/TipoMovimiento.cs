using System;
using System.Collections.Generic;

namespace CoderPadTest.Models;

public partial class TipoMovimiento
{
    public TipoMovimiento() { this.Descripcion = ""; }
    public int TipoMovimientoId { get; set; }

    public string Descripcion { get; set; }
}
