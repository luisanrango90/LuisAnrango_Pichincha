using LuisAnrango.Model;
using Microsoft.EntityFrameworkCore;

namespace LuisAnrango.Context
{
    public partial class BaseContext : DbContext
    {


        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClIdCliente);

                entity.ToTable("cliente");

                entity.Property(e => e.ClIdCliente).HasColumnName("cl_id_cliente");

                entity.Property(e => e.ClContrasenia)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cl_contrasenia");

                entity.Property(e => e.ClEstado).HasColumnName("cl_estado");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cl_direccion");

                entity.Property(e => e.Edad).HasColumnName("cl_edad");

               

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cl_genero");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cl_identificacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cl_nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cl_telefono");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.CuNumeroCuenta)
                    .HasName("PK__cuentas__5138EEC71FAE4CF6");

                entity.ToTable("cuentas");

                entity.Property(e => e.CuNumeroCuenta)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cu_numero_cuenta");

                entity.Property(e => e.CuIdCliente).HasColumnName("cu_id_cliente");

                entity.Property(e => e.CuTipoCuenta)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cu_tipo_cuenta");

                entity.Property(e => e.CuSaldoInicial)
                    .HasColumnType("money")
                    .HasColumnName("cu_saldo_inicial");

                entity.Property(e => e.CuEstado).HasColumnName("cu_estado");

                entity.HasOne(d => d.CuIdClienteNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.CuIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cuentas_cuentas");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.MoIdMovimiento)
                    .HasName("PK__movimien__2D96FD743EDEFBC6");

                entity.ToTable("movimientos");

                entity.Property(e => e.MoIdMovimiento).HasColumnName("mo_id_movimiento");

                entity.Property(e => e.MoFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("mo_fecha");

                entity.Property(e => e.MoNumeroCuenta)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("mo_numero_cuenta");

                entity.Property(e => e.MoSaldo)
                    .HasColumnType("money")
                    .HasColumnName("mo_saldo");

                entity.Property(e => e.MoTipoMovimiento)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("mo_tipo_movimiento");

                entity.Property(e => e.MoValor)
                    .HasColumnType("money")
                    .HasColumnName("mo_valor");

                entity.HasOne(d => d.MoNumeroCuentaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MoNumeroCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movimientos_cuentas");
            });


        }
    }
}
