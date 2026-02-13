using Microsoft.AspNetCore.Mvc;
using ProyectoVentas.Models;
using ProyectoVentas.Data; // ajusta si tu namespace es otro
using Microsoft.EntityFrameworkCore;/// en mi caso agregado para que funcinen los metodos asyncronos
using ProyectoVentas.Helpers; // mis catalogos estan aqui

namespace ProyectoVentas.Controllers
{
    public class VentasController : Controller
    {


        /////////////////////Es tu puerta a la base de datos.///////////////////////////////////////////////////////
         private readonly ApplicationDbContext _context;
         public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }



                                                            /* PRIMERA VERSION DE MI METODO INDEX EN APARTADO VENTAS
       public IActionResult Index()
        {
                       //var ventas = new List<Venta>
                        {
                            new Venta
                            {
                                Id = 1,
                                Articulo = "iPad Air 4",
                                FechaVenta = DateTime.Now,
                                Facebook = "Cuenta 1",
                                PrecioVenta = 12000,
                                Inversion = 9500,
                                Ubicacion = "Ciudad Juárez",
                                Cliente = "Juan Pérez",
                                Comentario = "Entrega en punto medio"
                            }
                        };
            
           var ventas = _context.Ventas.ToList();
            return View();
        }*/

        // SEGUNDA VERSION USANDO ASINCRONO POR ESCALABILIDAD FUTURA Y MEJOR IMPLEMENTACION 
     public async Task<IActionResult> Index()
{
    var ventas = await _context.Ventas.ToListAsync();

    var hoy = DateOnly.FromDateTime(DateTime.Now);

    decimal metaDiaria = 3500m;

    // 🔹 Calcular inicio de semana (lunes)
int diff = (7 + (DateTime.Now.DayOfWeek - DayOfWeek.Monday)) % 7;
DateOnly inicioSemana = hoy.AddDays(-diff);


decimal gananciaSemana = ventas
    .Where(v =>
        v.FechaVenta.HasValue &&
        DateOnly.FromDateTime(v.FechaVenta.Value) >= inicioSemana &&
        DateOnly.FromDateTime(v.FechaVenta.Value) <= hoy
    )
    .Sum(v => v.PrecioVenta - v.Inversion) ?? 0m;









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

    return View(ventas);
}





        // OKEY MI PRIMER FORMULARIO FUNCIONAL V1
         [HttpGet]
        public IActionResult Create()
        {   
        CargarCatalogos();
            return View();
        }





                                                             /*[HttpPost]
        public IActionResult Create(Venta venta)
        {
            foreach (var entry in ModelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    Console.WriteLine(
                        $"CAMPO: {entry.Key} | ERROR: {error.ErrorMessage}"
                    );
                }
            }

            return View(venta);
        }*/
        // nueva actualizacion de metodo post funcinal ya con base de datos v1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Venta venta)
        {


            try{
            if (!ModelState.IsValid)
            {
                // 🔴 volver a cargar catálogos
                CargarCatalogos();
                return View(venta);
            }

            _context.Ventas.Add(venta);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return Content("ERROR AL GUARDAR: " + ex.Message);
            }
        }







        /*2.1 Método GET Edit Este método: Recibe el id Busca la venta en la DBLa manda a la vista*/
        public async Task<IActionResult> Edit(int? id)
        {
            CargarCatalogos();
            if (id == null)
            {
                return NotFound();
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);
        }






        /*2.2 Método POST Edit Este método: Recibe la venta editada Actualiza la base de datos*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venta venta)
        {
            if (id != venta.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // 🔴 OBLIGATORIO
            CargarCatalogos();

            return View(venta);
        }







        //3.1 implementacion del metodo, GET Delete (confirmación)
        public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var venta = await _context.Ventas
        .FirstOrDefaultAsync(v => v.Id == id);

    if (venta == null)
    {
        return NotFound();
    }

    return View(venta);
}






        //3.2 POST Delete (elimina de verdad)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);

            if (venta != null)
            {
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }



        private void CargarCatalogos()
        {
            ViewBag.Articulos = Catalogos.Articulos;
            ViewBag.Facebooks = Catalogos.Facebooks;
            ViewBag.Ubicaciones = Catalogos.Ubicaciones;
        }





    }
}
