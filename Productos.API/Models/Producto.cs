using System.ComponentModel.DataAnnotations;

namespace Productos.API.Models;

public class Producto
{
    public int Id { get; set; }

    [Required]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    public string Nombre { get; set; } = string.Empty;

    public decimal Precio { get; set; }
    public int Stock { get; set; }
}