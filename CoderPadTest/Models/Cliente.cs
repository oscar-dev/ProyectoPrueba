using System;
using System.Collections.Generic;

namespace CoderPadTest.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public int PersonaId { get; set; }

    public string Contrasenia { get; set; } = null!;

    public bool Estado { get; set; }
}
