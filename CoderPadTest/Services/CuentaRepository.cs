using CoderPadTest.DTOs;
using CoderPadTest.Exceptions;
using CoderPadTest.Interfaces;
using CoderPadTest.Models;

namespace CoderPadTest.Services
{
    public class CuentaRepository
        : ICuentaRepository, IDisposable
    {
        private BaseContext _baseContext;
        private bool disposed = false;

        public CuentaRepository(BaseContext baseContext)
        {
            _baseContext = baseContext;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._baseContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void insertCuenta(CuentaDTO cuentaDTO)
        {
            validarCuentaDTO(cuentaDTO);

            var cuenta = new Cuenta()
            {
                ClienteId = cuentaDTO.ClienteId,
                NroCuenta = cuentaDTO.NroCuenta,
                TipoCuentaId = cuentaDTO.TipoCuentaId,
                SaldoInicial = cuentaDTO.SaldoInicial,
                Saldo = cuentaDTO.SaldoInicial,         // El saldo al crear la cuenta es siempre el saldo inicial.
                LimiteDiario = cuentaDTO.LimiteDiario,
                Estado = cuentaDTO.Estado
            };

            this._baseContext.Cuentas.Add(cuenta);

            this._baseContext.SaveChanges();
        }
        private bool validarCuentaDTO(CuentaDTO cuentaDTO)
        {
            if (cuentaDTO.NroCuenta == null || cuentaDTO.NroCuenta.Trim().Length <= 0)
            {
                throw new Exception("Número de cuenta inválida");
            }
            if (cuentaDTO.ClienteId <= 0)
            {
                throw new Exception("Cliente inválido");
            }

            var cliente = this._baseContext.Clientes.Where(c => c.ClienteId == cuentaDTO.ClienteId).FirstOrDefault();

            if (cliente == null)
            {
                throw new NotFoundException("El cliente no existe");
            }

            if (cuentaDTO.TipoCuentaId <= 0)
            {
                throw new Exception("Tipo de cuenta inválido");
            }

            var cuenta = this._baseContext.TiposCuenta.Where(c => c.TipoCuentaId == cuentaDTO.TipoCuentaId).FirstOrDefault();

            if (cuenta == null)
            {
                throw new NotFoundException("El tipo de cuenta no existe");
            }

            return true;
        }
        public void updateCuenta(CuentaDTO cuentaDTO)
        {
            validarCuentaDTO(cuentaDTO);

            var cuenta = this._baseContext.Cuentas.Where(c => c.CuentaId == cuentaDTO.CuentaId).FirstOrDefault();

            if (cuenta != null)
            {
                cuenta.ClienteId = cuentaDTO.ClienteId;
                cuenta.NroCuenta = cuentaDTO.NroCuenta;
                cuenta.TipoCuentaId = cuentaDTO.TipoCuentaId;
                cuenta.SaldoInicial = cuentaDTO.SaldoInicial;
                cuenta.Saldo = cuentaDTO.Saldo;
                cuenta.LimiteDiario = cuentaDTO.LimiteDiario;
                cuenta.Estado = cuentaDTO.Estado;

                this._baseContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException("Cuenta no encontrada. Id: " + cuentaDTO.CuentaId);
            }
        }
        public void deleteCuenta(int cuentaId)
        {
            var cuenta = this._baseContext.Cuentas.Where(c => c.CuentaId == cuentaId).FirstOrDefault();

            if (cuenta != null)
            {
                this._baseContext.Cuentas.Remove(cuenta);
                this._baseContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException("Cuenta no encontrada. Id: " + cuentaId);
            }
        }
        public IList<CuentaDTO> getCuentas()
        {
            using (var context = this._baseContext)
            {
                var cuentas = from c in context.Cuentas
                              join t in context.TiposCuenta on c.TipoCuentaId equals t.TipoCuentaId
                              select new CuentaDTO
                              {
                                  CuentaId = c.CuentaId,
                                  NroCuenta = c.NroCuenta,
                                  Estado = c.Estado,
                                  ClienteId = c.ClienteId,
                                  SaldoInicial = c.SaldoInicial,
                                  Saldo = c.Saldo,
                                  LimiteDiario = c.LimiteDiario,
                                  TipoCuentaId = c.TipoCuentaId,
                                  TipoCuenta = t.Descripcion
                              };

                return cuentas.ToList();
            }
        }
        public CuentaDTO getCuentaById(int cuentaId)
        {
            using (var context = this._baseContext)
            {
                var cuenta = (from c in context.Cuentas
                              join t in context.TiposCuenta on c.TipoCuentaId equals t.TipoCuentaId
                              where c.CuentaId == cuentaId
                                select new CuentaDTO
                                {
                                      CuentaId = c.CuentaId,
                                      NroCuenta = c.NroCuenta,
                                      Estado = c.Estado,
                                      ClienteId = c.ClienteId,
                                      SaldoInicial = c.SaldoInicial,
                                      Saldo = c.Saldo,
                                      LimiteDiario = c.LimiteDiario,
                                      TipoCuentaId = c.TipoCuentaId,
                                        TipoCuenta = t.Descripcion
                                }).Take(1)
                                .FirstOrDefault();

                if (cuenta == null)
                {
                    throw new NotFoundException("Cuenta no encontrada. Id: " + cuentaId);
                }

                return cuenta;
            }
        }
        public IList<TipoCuentaDTO> getTiposCuenta()
        {
            using (var context = this._baseContext)
            {
                var tiposCuenta = from tc in context.TiposCuenta
                              select new TipoCuentaDTO
                              {
                                  TipoCuentaId = tc.TipoCuentaId,
                                  Descripcion = tc.Descripcion,
                              };

                return tiposCuenta.ToList();
            }
        }
    }
}
