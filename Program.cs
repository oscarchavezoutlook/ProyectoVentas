using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 MVC con vistas
builder.Services.AddControllersWithViews();

// 🔹 Ruta absoluta y segura para SQLite
var dbPath = Path.Combine(AppContext.BaseDirectory, "ventas.db");

// 🔹 Registrar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}")
);

var app = builder.Build();

// 🔴 APLICAR MIGRACIONES AL ARRANCAR (CLAVE)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    db.Database.Migrate();
}

// 🔹 Manejo de errores
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 🔹 Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// 🔹 Puerto para Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
