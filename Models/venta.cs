using System;
using System.ComponentModel.DataAnnotations;
namespace ProyectoVentas.Models
{
    public class Venta
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El artículo es obligatorio")]
    public string Articulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de venta es obligatoria")]
    public DateTime? FechaVenta { get; set; }

    [Required(ErrorMessage = "Facebook es obligatorio")]
    public string Facebook { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio de venta es obligatorio")]
    public decimal? PrecioVenta { get; set; }

    [Required(ErrorMessage = "La inversión es obligatoria")]
    public decimal? Inversion { get; set; }

    [Required(ErrorMessage = "La ubicación es obligatoria")]
    public string Ubicacion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El cliente es obligatorio")]
    public string Cliente { get; set; } = string.Empty;

    // ✅ ÚNICO OPCIONAL
    public string? Comentario { get; set; }

    // Cálculo seguro
    public decimal GananciaIndividual =>
        (PrecioVenta ?? 0) - (Inversion ?? 0);
}

}
