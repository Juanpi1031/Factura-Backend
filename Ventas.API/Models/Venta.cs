namespace Ventas.API.Models;

public class DetalleVenta
{
    public int Id { get; set; }
    public int VentaId { get; set; }
    public string ProductoCodigo { get; set; } = string.Empty;
    public string ProductoNombre { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal => Cantidad * PrecioUnitario;
}

public class Venta
{
    public int Id { get; set; }
    public string ClienteCedula { get; set; } = string.Empty;
    public string ClienteNombre { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public List<DetalleVenta> Detalles { get; set; } = new();
    public decimal Subtotal => Detalles.Sum(d => d.Subtotal);
    public decimal Iva => Math.Round(Subtotal * 0.15m, 2);
    public decimal Total => Subtotal + Iva;
}