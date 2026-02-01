
using Microsoft.AspNetCore.Mvc;
using ProyectoVentas.Models;

namespace ProyectoVentas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var ventas = new List<Venta>(); // por ahora vacío
            return View(ventas);
        }
    }
}
