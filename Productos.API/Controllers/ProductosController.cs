using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos.API.Data;
using Productos.API.Models;

namespace Productos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ProductosDbContext _db;
    public ProductosController(ProductosDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetAll()
        => Ok(await _db.Productos.ToListAsync());

    [HttpGet("{codigo}")]
    public async Task<ActionResult<Producto>> GetByCodigo(string codigo)
    {
        var producto = await _db.Productos.FirstOrDefaultAsync(p => p.Codigo == codigo);
        if (producto is null) return NotFound();
        return Ok(producto);
    }

    [HttpPost]
public async Task<ActionResult<Producto>> Create(Producto producto)
{
    var existe = await _db.Productos.AnyAsync(p => p.Codigo == producto.Codigo);
    if (existe) return Conflict(new { mensaje = $"Ya existe un producto con el código {producto.Codigo}" });

    _db.Productos.Add(producto);
    await _db.SaveChangesAsync();
    return CreatedAtAction(nameof(GetByCodigo), new { codigo = producto.Codigo }, producto);
}
}