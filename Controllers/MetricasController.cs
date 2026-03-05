using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Data;
using ProyectoVentas.Models;

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

            return View(ventas);

             throw new Exception("PROBANDO ERROR");
        }
    }
}