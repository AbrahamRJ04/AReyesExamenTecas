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
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-AVT6ORL\\SQLEXPRESS;Initial Catalog=AReyesTecasExamen;Integrated Security=True; Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D594664257538D99");

            entity.ToTable("Cliente", tb => tb.HasTrigger("ClienteNumeroClinete"));

            entity.HasIndex(e => e.NumeroCliente, "UQ__Cliente__28D65826C0FD58AC").IsUnique();

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
                .HasConstraintName("FK__Cliente__IdRol__44FF419A");
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.IdNumeroCuenta).HasName("PK__Cuenta__A0B313B59BFB5FE1");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Cuenta__IdClient__45F365D3");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CDF2A27F8");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoTransaccion>(entity =>
        {
            entity.HasKey(e => e.IdTipoTransaccion).HasName("PK__TipoTran__FE769C8DC3A3E516");

            entity.ToTable("TipoTransaccion");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__Transacc__334B1F77FD608307");

            entity.ToTable("Transaccion");

            entity.Property(e => e.Detalle).IsUnicode(false);
            entity.Property(e => e.FechaTransaccion).HasColumnType("datetime");
            entity.Property(e => e.MontoTransaccion).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.IdNumeroCuenta)
                .HasConstraintName("FK__Transacci__IdCue__47DBAE45");

            entity.HasOne(d => d.IdTipoTransaccionNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.IdTipoTransaccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__IdTip__46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
