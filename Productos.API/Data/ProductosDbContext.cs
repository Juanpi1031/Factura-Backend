using Microsoft.EntityFrameworkCore;
using Productos.API.Models;

namespace Productos.API.Data;

public class ProductosDbContext : DbContext
{
    public ProductosDbContext(DbContextOptions<ProductosDbContext> options) : base(options) { }

    public DbSet<Producto> Productos => Set<Producto>();

   protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Producto>()
        .HasIndex(p => p.Codigo)
        .IsUnique();

    modelBuilder.Entity<Producto>().HasData(
        new Producto { Id = 1, Codigo = "P001", Nombre = "Laptop HP 15\"", Precio = 850, Stock = 10 },
        new Producto { Id = 2, Codigo = "P002", Nombre = "Mouse inalámbrico", Precio = 25, Stock = 50 },
        new Producto { Id = 3, Codigo = "P003", Nombre = "Teclado mecánico", Precio = 75, Stock = 30 },
        new Producto { Id = 4, Codigo = "P004", Nombre = "Monitor 24\"", Precio = 320, Stock = 15 }
    );
}
}