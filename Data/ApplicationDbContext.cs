using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Models;

namespace ProyectoVentas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
 
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaPendiente> VentasPendientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Venta>()
                .Property(v => v.FechaVenta)
                .HasColumnType("timestamp without time zone");
        }
    }
}
