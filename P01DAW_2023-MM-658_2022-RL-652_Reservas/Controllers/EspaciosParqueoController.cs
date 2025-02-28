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
        public IActionResult GetAll()
        {
            var espacios = _context.espacioParqueos.ToList();
            return Ok(espacios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var espacio = _context.espacioParqueos.Find(id);
            if (espacio == null)
            {
                return NotFound();
            }
            return Ok(espacio);
        }

        
        [HttpPost]
        public IActionResult Create([FromBody] EspacioParqueos espacioParqueo)
        {
            if (espacioParqueo == null)
            {
                return BadRequest();
            }

            _context.espacioParqueos.Add(espacioParqueo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = espacioParqueo.Id }, espacioParqueo);
        }

      
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EspacioParqueos espacioParqueo)
        {
            if (id != espacioParqueo.Id)
            {
                return BadRequest();
            }

            var existingEspacio = _context.espacioParqueos
                .Find(id);
            if (existingEspacio == null)
            {
                return NotFound();
            }

            existingEspacio.Numero = espacioParqueo.Numero;
            existingEspacio.Ubicacion = espacioParqueo.Ubicacion;
            existingEspacio.CostoPorHora = espacioParqueo.CostoPorHora;
            existingEspacio.Estado = espacioParqueo.Estado;

            _context.SaveChanges();
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var espacioParqueo = _context.espacioParqueos.Find(id);
            if (espacioParqueo == null)
            {
                return NotFound();
            }

            _context.espacioParqueos.Remove(espacioParqueo);
            _context.SaveChanges();
            return NoContent();
        }

        
        [HttpGet("buscar")]
        public IActionResult Search([FromQuery] string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                return BadRequest();
            }

            var espacios = _context.espacioParqueos
                                   .Where(e => e.Estado.Contains(estado))
                                   .ToList();

            if (espacios.Count == 0)
            {
                return NotFound();
            }

            return Ok(espacios);
        }
    }
    
}
