using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Data;
using ProyectoVentas.Helpers;
using ProyectoVentas.Models;
using static ProyectoVentas.Helpers.DateNowJuarez;

namespace ProyectoVentas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas.ToListAsync();

            var hoy = HoyJuarez();

            decimal metaDiaria = 3500m;

            decimal gananciaHoy = ventas
                .Where(v => v.FechaVenta.HasValue &&
                            DateOnly.FromDateTime(v.FechaVenta.Value) == hoy)
                .Sum(v => v.PrecioVenta - v.Inversion) ?? 0m;

            decimal progreso = metaDiaria == 0
                ? 0
                : Math.Min(Math.Round((gananciaHoy / metaDiaria) * 100, 0), 100);

            ViewBag.GananciaHoy = gananciaHoy;
            ViewBag.MetaDiaria = metaDiaria;
            ViewBag.FaltaParaMeta = metaDiaria - gananciaHoy;
            ViewBag.Progreso = progreso;

            // Ventas del día (máx 5, más recientes)
            ViewBag.VentasHoy = ventas
                .Where(v => v.FechaVenta.HasValue &&
                            DateOnly.FromDateTime(v.FechaVenta.Value) == hoy)
                .OrderByDescending(v => v.Id)
                .Take(5)
                .ToList();

            return View();
        }
    }
}
