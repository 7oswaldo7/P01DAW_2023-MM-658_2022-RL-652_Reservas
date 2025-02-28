using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P01DAW_2023_MM_658_2022_RL_652_Reservas.Models;

namespace P01DAW_2023_MM_658_2022_RL_652_Reservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestorUsuariosController : ControllerBase
    {
        private readonly ReservasContext _reservasContexto;
        public GestorUsuariosController(ReservasContext reservasContexto)
        {
            _reservasContexto = reservasContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var listadoUsuario = (from e in _reservasContexto.usuarios select e);


            if (listadoUsuario.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoUsuario);
        }

        [HttpGet]
        [Route("LogIn")]
        public IActionResult LogIn(string email, string pass)
        {
            var validacionUsuario = (from e in _reservasContexto.usuarios
                                  where e.Contrasena == pass && e.Correo == email
                                  select e).FirstOrDefault();


            if (validacionUsuario == null)
            {
                return Unauthorized("Correo o contraseña invalidos");
            }
            else
                return Ok($"Bienvenido/a {validacionUsuario.Nombre}");

        }
    }
}
