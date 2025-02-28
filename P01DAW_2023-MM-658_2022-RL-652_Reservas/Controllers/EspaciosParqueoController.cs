using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01DAW_2023_MM_658_2022_RL_652_Reservas.Models;

namespace P01DAW_2023_MM_658_2022_RL_652_Reservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspaciosParqueoController : ControllerBase
    {

        private readonly ReservasContext _context;

        public EspaciosParqueoController(ReservasContext context)
        {
            _context = context;
        }
        [HttpGet("GetAll")]



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var espacio = await _context.espacioParqueos.FindAsync(id);
            if (espacio == null)
            {
                return NotFound();
            }
            return Ok(espacio);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EspacioParqueos espacioParqueo)
        {
            if (espacioParqueo == null)
            {
                return BadRequest();
            }

            _context.espacioParqueos.Add(espacioParqueo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = espacioParqueo.Id }, espacioParqueo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EspacioParqueos espacioParqueo)
        {
            if (id != espacioParqueo.Id)
            {
                return BadRequest();
            }

            var existingEspacio = await _context.espacioParqueos.FindAsync(id);
            if (existingEspacio == null)
            {
                return NotFound();
            }

            existingEspacio.Numero = espacioParqueo.Numero;
            existingEspacio.Ubicacion = espacioParqueo.Ubicacion;
            existingEspacio.CostoPorHora = espacioParqueo.CostoPorHora;
            existingEspacio.Estado = espacioParqueo.Estado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var espacioParqueo = await _context.espacioParqueos.FindAsync(id);
            if (espacioParqueo == null)
            {
                return NotFound();
            }

            _context.espacioParqueos.Remove(espacioParqueo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Search([FromQuery] string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                return BadRequest();
            }

            var espacios = await _context.espacioParqueos
                                         .Where(e => e.Estado.Contains(estado))
                                         .ToListAsync();

            if (!espacios.Any())
            {
                return NotFound();
            }

            return Ok(espacios);
        }
    }
    
}
