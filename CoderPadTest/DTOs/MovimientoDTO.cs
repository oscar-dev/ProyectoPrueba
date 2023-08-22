namespace CoderPadTest.DTOs
{
    public class MovimientoDTO
    {
        public MovimientoDTO() { this.TipoMovimiento = ""; }
        public int MovimientoId { get; set; }
        public int CuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoId { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}
