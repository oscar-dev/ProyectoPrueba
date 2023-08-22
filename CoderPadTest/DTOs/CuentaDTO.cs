namespace CoderPadTest.DTOs
{
    public class CuentaDTO
    {
        public CuentaDTO()
        {
            this.NroCuenta = "";
            this.TipoCuenta = "";
        }
        public int CuentaId { get; set; }
        public int ClienteId { get; set; }
        public string NroCuenta { get; set; }
        public int TipoCuentaId { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Saldo { get; set; }
        public decimal LimiteDiario { get; set; }
        public bool Estado { get; set; }
    }
}
