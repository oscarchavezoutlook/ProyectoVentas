using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoVentas.Models
{
    public class VentaPendiente
    {
        public int Id { get; set; }

        [Required]
        public string Articulo { get; set; } = string.Empty;    

        [Required]
        public string NombreCliente { get; set; } = string.Empty;

        [Required]
        public string Ubicacion { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
