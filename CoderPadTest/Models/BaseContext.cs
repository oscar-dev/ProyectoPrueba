using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoderPadTest.Models;

public partial class BaseContext : DbContext
{
    public BaseContext()
    {
    }

    public BaseContext(DbContextOptions<BaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<TipoCuenta> TiposCuenta { get; set; }

    public virtual DbSet<TipoMovimiento> TiposMovimiento { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("clientes");

            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.PersonaId).HasColumnName("personaId");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.ToTable("cuentas");

            entity.Property(e => e.CuentaId).HasColumnName("cuentaId");
            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.LimiteDiario)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("limiteDiario");
            entity.Property(e => e.NroCuenta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nroCuenta");
            entity.Property(e => e.Saldo)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("saldo");
            entity.Property(e => e.SaldoInicial)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("saldoInicial");
            entity.Property(e => e.TipoCuentaId).HasColumnName("tipoCuentaId");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.ToTable("movimientos");

            entity.Property(e => e.MovimientoId).HasColumnName("movimientoId");
            entity.Property(e => e.CuentaId).HasColumnName("cuentaId");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Saldo)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("saldo");
            entity.Property(e => e.TipoId).HasColumnName("tipoId");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("valor");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.ToTable("personas");

            entity.Property(e => e.PersonaId).HasColumnName("personaId");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genero");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<TipoCuenta>(entity =>
        {
            entity.HasKey(e => e.TipoCuentaId);

            entity.ToTable("tipos_cuenta");

            entity.Property(e => e.TipoCuentaId).HasColumnName("tipoCuentaId");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoMovimiento>(entity =>
        {
            entity.HasKey(e => e.TipoMovimientoId).HasName("PK_tipos_movimientos");

            entity.ToTable("tipos_movimiento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
