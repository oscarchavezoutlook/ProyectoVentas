using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProyectoVentas.Data;

namespace ProyectoVentas.Data
{
    public class ApplicationDbContextFactory 
        : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlite("Data Source=ventas.db");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
