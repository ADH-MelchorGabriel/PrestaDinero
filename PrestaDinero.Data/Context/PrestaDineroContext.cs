
using Microsoft.EntityFrameworkCore;
using PrestaDinero.Core;

namespace PrestaDinero.Data.Context
{
    public class  PrestaDineroContext :DbContext
    {
        public PrestaDineroContext( DbContextOptions<PrestaDineroContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteEntity>().HasMany(s => s.Vales).WithOne(s => s.Cliente).HasForeignKey(c => c.IdCliente);
            modelBuilder.Entity<DistribuidorEntity>().HasMany(s => s.Vales).WithOne(s => s.Distribuidor).HasForeignKey(c => c.IdDistribuidor);
            modelBuilder.Entity<TipoPrestamoEntity>().HasMany(s => s.Vales).WithOne(s => s.TipoPrestamo).HasForeignKey(c => c.IdTipoPrestamo);

            modelBuilder.Entity<ValeEntity>().HasMany(s => s.ValeDetalle).WithOne(s => s.Vale).HasForeignKey(c => c.IdVale);
        }
        public DbSet<TipoPrestamoEntity> TipoPrestamo { get; set; }
        public DbSet<ClienteEntity> Cliente { get; set; }
        public DbSet<DistribuidorEntity> Distribuidor { get; set; }
        public DbSet<ValeEntity> Vale { get; set; }
        public DbSet<ValeDetalleEntity> ValeDetalle { get; set; }
        public DbSet<TablaInteresEntity> TablaInteres { get; set; }

    }
}
