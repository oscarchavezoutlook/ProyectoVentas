using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Data;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// 🔴 LEER DATABASE_URL DE RAILWAY
var databaseUrl = Environment.GetEnvironmentVariable("postgresql://postgres:AEUhrowHAKrwgsajNDsakFoYAyfvtVyx@postgres.railway.internal:5432/railway");

if (string.IsNullOrEmpty(databaseUrl))
{
    throw new Exception("DATABASE_URL no está configurada.");
}

// 🔹 CONVERTIR DATABASE_URL A CONNECTION STRING REAL
var uri = new Uri(databaseUrl);
var userInfo = uri.UserInfo.Split(':');

var connectionStringBuilder = new NpgsqlConnectionStringBuilder
{
    Host = uri.Host,
    Port = uri.Port,
    Username = userInfo[0],
    Password = userInfo[1],
    Database = uri.AbsolutePath.Trim('/'),
    SslMode = SslMode.Require,
  //  TrustServerCertificate = true
};

var connectionString = connectionStringBuilder.ConnectionString;

// 🔹 REGISTRAR DB CONTEXT
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
);

var app = builder.Build();

// 🔹 APLICAR MIGRACIONES
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
