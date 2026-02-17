using Microsoft.AspNetCore.Mvc;
using ProyectoVentas.Data;
using ProyectoVentas.Models;
using Microsoft.EntityFrameworkCore;

public class VentasPendientesController : Controller
{
    private readonly ApplicationDbContext _context;

    public VentasPendientesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(VentaPendiente pendiente)
    {
        if (!ModelState.IsValid)
            return View(pendiente);

        _context.Add(pendiente);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var pendiente = await _context.VentasPendientes.FindAsync(id);
        if (pendiente != null)
        {
            _context.VentasPendientes.Remove(pendiente);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Home");
    }
}
