
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Data;
/////////////////////////////////////////////////////////////////



var builder = WebApplication.CreateBuilder(args);

// 🔴 MVC con Vistas (OBLIGATORIO)
builder.Services.AddControllersWithViews();

//“Oye, voy a usar SQLite y esta es mi base”.   ventas.db será un archivo físico en el proyecto.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=ventas.db"));

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

app.Run();
