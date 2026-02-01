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
    }
}
