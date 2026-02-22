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

    [HttpPost]
    public async Task<IActionResult> Create(VentaPendiente pendiente)
    {
        pendiente.FechaCreacion = DateTime.UtcNow;

        _context.VentasPendientes.Add(pendiente);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await _context.VentasPendientes.FindAsync(id);
        if (p != null)
        {
            _context.VentasPendientes.Remove(p);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Home");
    }
}