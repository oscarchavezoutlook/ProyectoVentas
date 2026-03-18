using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Data;
using ProyectoVentas.Models;
using static ProyectoVentas.Helpers.DateNowJuarez;

namespace ProyectoVentas.Controllers
{
    public class MetricasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MetricasController(ApplicationDbContext context)
        {
            _context = context;
        }

public async Task<IActionResult> Index()
{
    var ventas = await _context.Ventas.ToListAsync();

    var hoy = HoyJuarez();
    var inicioSemana = hoy.AddDays(-(int)hoy.DayOfWeek);


DateTime inicioSemanaDT = inicioSemana.ToDateTime(TimeOnly.MinValue);
DateTime hoyDT = hoy.ToDateTime(TimeOnly.MinValue);




    var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
    var inicioAnio = new DateTime(hoy.Year, 1, 1);

    var modelo = new MetricasViewModel
    {
    

            GananciaHoy  = ventas
            .Where(v => v.FechaVenta.HasValue &&
                        DateOnly.FromDateTime(v.FechaVenta.Value) == hoy)
            .Sum(v => v.PrecioVenta - v.Inversion) ?? 0m,

            VentasHoy = ventas
                .Count(v => v.FechaVenta.HasValue && 
                DateOnly.FromDateTime(v.FechaVenta.Value) == hoy),

            GananciaSemana = ventas
            .Where(v => v.FechaVenta.HasValue &&
                        v.FechaVenta.Value >= inicioSemanaDT)
            .Sum(v => v.PrecioVenta - v.Inversion) ?? 0m,

            GananciaMes = ventas
            .Where(v => v.FechaVenta.HasValue &&
                        v.FechaVenta.Value >= inicioMes)
            .Sum(v => v.PrecioVenta - v.Inversion) ?? 0m,

            GananciaAnio = ventas
            .Where(v => v.FechaVenta.HasValue &&
                        v.FechaVenta.Value >= inicioAnio)
            .Sum(v => v.PrecioVenta - v.Inversion) ?? 0m,


       
    };

    return View(modelo);
}
    }
}