
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Data;
/////////////////////////////////////////////////////////////////



var builder = WebApplication.CreateBuilder(args);

// 🔴 MVC con Vistas (OBLIGATORIO)
builder.Services.AddControllersWithViews();

// 🔹 RUTA ABSOLUTA PARA SQLITE
var dbPath = Path.Combine(AppContext.BaseDirectory, "ventas.db");

// 🔹 REGISTRO DEL CONTEXT
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}")
);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 🔴 Ruta por defecto MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");

