using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clientes.API.Data;
using Clientes.API.Models;

namespace Clientes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClientesDbContext _db;
    public ClientesController(ClientesDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        => Ok(await _db.Clientes.ToListAsync());

    [HttpGet("{cedula}")]
    public async Task<ActionResult<Cliente>> GetByCedula(string cedula)
    {
        var cliente = await _db.Clientes.FirstOrDefaultAsync(c => c.Cedula == cedula);
        if (cliente is null) return NotFound(new { mensaje = "Cliente no encontrado" });
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> Create(Cliente cliente)
    {
        var existe = await _db.Clientes.AnyAsync(c => c.Cedula == cliente.Cedula);
        if (existe) return Conflict(new { mensaje = "Ya existe un cliente con esa cédula" });

        cliente.FechaCreacion = DateTime.UtcNow;
        _db.Clientes.Add(cliente);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByCedula), new { cedula = cliente.Cedula }, cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Cliente cliente)
    {
        if (id != cliente.Id) return BadRequest();
        _db.Entry(cliente).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }
}