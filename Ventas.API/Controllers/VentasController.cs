using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ventas.API.Data;
using Ventas.API.Models;

namespace Ventas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly VentasDbContext _db;
    public VentasController(VentasDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venta>>> GetAll()
        => Ok(await _db.Ventas.Include(v => v.Detalles).ToListAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Venta>> GetById(int id)
    {
        var venta = await _db.Ventas.Include(v => v.Detalles).FirstOrDefaultAsync(v => v.Id == id);
        if (venta is null) return NotFound();
        return Ok(venta);
    }

    [HttpPost]
    public async Task<ActionResult<Venta>> Create(Venta venta)
    {
        venta.Fecha = DateTime.UtcNow;
        _db.Ventas.Add(venta);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = venta.Id }, venta);
    }
}