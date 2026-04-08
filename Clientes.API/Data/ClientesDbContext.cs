using Microsoft.EntityFrameworkCore;
using Clientes.API.Models;

namespace Clientes.API.Data;

public class ClientesDbContext : DbContext
{
    public ClientesDbContext(DbContextOptions<ClientesDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.Cedula)
            .IsUnique();

        // Datos iniciales de prueba
        modelBuilder.Entity<Cliente>().HasData(
    new Cliente { Id = 1, Cedula = "0912345678", Nombre = "Juan Carlos", Apellido = "Pérez Mora", Telefono = "0991234567", Correo = "juan.perez@gmail.com", Direccion = "Av. Cevallos 230, Ambato", FechaCreacion = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
    new Cliente { Id = 2, Cedula = "1712345678", Nombre = "María", Apellido = "López Torres", Telefono = "0987654321", Correo = "maria.lopez@gmail.com", Direccion = "Calle Bolívar 45, Quito", FechaCreacion = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
);
    }
}