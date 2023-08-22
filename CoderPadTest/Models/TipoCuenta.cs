using System;
using System.Collections.Generic;

namespace CoderPadTest.Models;

public partial class TipoCuenta
{
    public int TipoCuentaId { get; set; }

    public string Descripcion { get; set; } = null!;
}
