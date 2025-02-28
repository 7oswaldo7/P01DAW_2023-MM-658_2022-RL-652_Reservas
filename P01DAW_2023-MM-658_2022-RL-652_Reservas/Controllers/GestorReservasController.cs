using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01DAW_2023_MM_658_2022_RL_652_Reservas.Models;

namespace P01DAW_2023_MM_658_2022_RL_652_Reservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestorReservasController : ControllerBase
    {
        private readonly ReservasContext _reservasContexto;
        public GestorReservasController(ReservasContext reservasContexto)
        {
            _reservasContexto = reservasContexto;
        }

        [HttpGet]
        [Route("GetReservasActivas")]
        public IActionResult GetAll(int id)
        {
            var listadoReservas = (from e in _reservasContexto.reservas where e.UsuarioId == id select e);

            if (listadoReservas.Count() == 0)
                return NotFound();

            return Ok(listadoReservas);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddReserva(int IDUsuario,int IDEspacioParqueo,DateTime FechaHoraInicio,int HorasReservadas)
        {
            try
            {
                Reservas reserva = new Reservas();
                reserva.UsuarioId = IDUsuario;
                reserva.EspacioParqueoId = IDEspacioParqueo;
                reserva.FechaHoraInicio = FechaHoraInicio;
                reserva.HorasReservadas = HorasReservadas;
                reserva.Estado = "Activo";

                EspacioParqueos? reservacion = (from e in _reservasContexto.espacioParqueos where e.Id == reserva.EspacioParqueoId select e).FirstOrDefault();

                if (reservacion != null && reservacion.Estado == "Disponible")
                {
                    _reservasContexto.reservas.Add(reserva);
                    reservacion.Estado = "Reservado";
                    _reservasContexto.Entry(reservacion).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _reservasContexto.SaveChanges();
                    return Ok(reserva);
                }
                else
                    return Unauthorized("El espacio no existe o no esta disponible");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult CancelarReserva(int id)
        {
            Reservas? reservaCancelar = (from e in _reservasContexto.reservas where e.Id == id select e).FirstOrDefault();

            if (reservaCancelar == null)
            {
                return NotFound();
            }

            if (reservaCancelar.FechaHoraInicio > DateTime.Now)
            {
                
                EspacioParqueos espacio = (from e in _reservasContexto.espacioParqueos where e.Id == reservaCancelar.EspacioParqueoId select e).FirstOrDefault();
                espacio.Estado = "Disponible";
                _reservasContexto.Entry(espacio).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _reservasContexto.reservas.Attach(reservaCancelar);
                _reservasContexto.reservas.Remove(reservaCancelar);
                _reservasContexto.SaveChanges();
                return Ok(reservaCancelar);
            }
            else
                return Unauthorized("La reservacion ha pasado su tiempo para cancelacion se debio cancelar antes de " + reservaCancelar.FechaHoraInicio);

        }
    }
}
