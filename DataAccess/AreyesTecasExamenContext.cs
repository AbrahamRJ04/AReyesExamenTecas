using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class AreyesTecasExamenContext : DbContext
{
    public AreyesTecasExamenContext()
    {
    }

    public AreyesTecasExamenContext(DbContextOptions<AreyesTecasExamenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<TipoTransaccion> TipoTransaccions { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-2POFP3ID; Database= AReyesTecasExamen; Trusted_Connection=True; User ID=sa; Password=pass@word1; Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D5946642003A1A9E");

            entity.ToTable("Cliente");

            entity.HasIndex(e => e.NumeroCliente, "UQ__Cliente__28D658263E1A2519").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Imagen).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroCliente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Cliente__IdRol__1367E606");
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.IdNumeroCuenta).HasName("PK__Cuenta__A0B313B54C7DC23A");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Cuenta__IdClient__164452B1");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__B7702B004446CBD7");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoTransaccion>(entity =>
        {
            entity.HasKey(e => e.IdTipoTransaccion).HasName("PK__TipoTran__FE769C8DF3D9FEAD");

            entity.ToTable("TipoTransaccion");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__Transacc__334B1F77B3DE923F");

            entity.ToTable("Transaccion");

            entity.Property(e => e.Detalle).IsUnicode(false);
            entity.Property(e => e.FechaTransaccion).HasColumnType("datetime");
            entity.Property(e => e.MontoTransaccion).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.IdCuenta)
                .HasConstraintName("FK__Transacci__IdCue__1BFD2C07");

            entity.HasOne(d => d.IdTipoTransaccionNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.IdTipoTransaccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__IdTip__1B0907CE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
