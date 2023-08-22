namespace CoderPadTest.DTOs
{
    public class RptMovimientoDTO
    {
        public RptMovimientoDTO()
        {
            this.Cliente = "";
            this.Cuenta = "";
            this.Tipo = "";
        }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Cuenta { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public decimal Movimiento { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
