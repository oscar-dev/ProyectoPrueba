using System;
using System.Collections.Generic;

namespace CoderPadTest.Models;

public partial class Persona
{
    public int PersonaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public int Edad { get; set; }

    public string Identificacion { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;
}
