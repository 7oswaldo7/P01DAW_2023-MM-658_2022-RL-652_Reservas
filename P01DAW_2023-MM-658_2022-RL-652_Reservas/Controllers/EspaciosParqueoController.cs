using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPractica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01DAW_2023_MM_658_2022_RL_652_Reservas.Models;

namespace P01DAW_2023_MM_658_2022_RL_652_Reservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspaciosParqueoController : ControllerBase
    {

        private readonly  ReservasContext _context;

        public EspaciosParqueoController(ReservasContext context)
        {
            _context = context;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<EspacioParqueo>>> GetEspacioParqueo()
        {
            return await _context.espacioParqueos.ToListAsync();

        }

        [HttpGet("id")]
        public async Task<ActionResult<EspacioParqueo>> GetEspacioParqueo(int id) 
        {
            var espacio = await _context.espacioParqueos.FindAsync(id);

            if (espacio == null) 
            {
                return NotFound();
            
            }

            return espacio;
        
        }

        [HttpPost]
        public async Task<ActionResult<EspacioParqueo>> CreateEspacioParqueo(EspacioParqueo espacioParqueo)
        {
            _context.espacioParqueos.Add(espacioParqueo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEspacioParqueo), new { id = espacioParqueo.Id }, espacioParqueo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEspacioParqueo(int id, EspacioParqueo espacioParqueo)
        {
            if (id != espacioParqueo.Id)
            {
                return BadRequest();
            }

            _context.Entry(espacioParqueo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.espacioParqueos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspacioParqueo(int id)
        {
            var espacioParqueo = await _context.espacioParqueos
                .FindAsync(id);
            if (espacioParqueo == null)
            {
                return NotFound();
            }

            _context.espacioParqueos.Remove(espacioParqueo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}
