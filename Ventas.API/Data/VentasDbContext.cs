using Microsoft.EntityFrameworkCore;
using Ventas.API.Models;

namespace Ventas.API.Data;

public class VentasDbContext : DbContext
{
    public VentasDbContext(DbContextOptions<VentasDbContext> options) : base(options) { }

    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<DetalleVenta> Detalles => Set<DetalleVenta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleVenta>()
            .Ignore(d => d.Subtotal);

        modelBuilder.Entity<Venta>()
            .Ignore(v => v.Subtotal)
            .Ignore(v => v.Iva)
            .Ignore(v => v.Total);
    }
}