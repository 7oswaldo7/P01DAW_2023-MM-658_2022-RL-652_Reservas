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
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] Usuarios usuario)
        {
            try
            {
                _reservasContexto.usuarios.Add(usuario);
                _reservasContexto.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] Usuarios UsuarioModificar)
        {
            Usuarios? usuarioActual = (from e in _reservasContexto.usuarios where e.Id == id select e).FirstOrDefault();

            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.Nombre = UsuarioModificar.Nombre;
            usuarioActual.Rol = UsuarioModificar.Rol;
            usuarioActual.Telefono = UsuarioModificar.Telefono;
            usuarioActual.Correo = UsuarioModificar.Correo;
            usuarioActual.Contrasena = UsuarioModificar.Contrasena;

            _reservasContexto.Entry(usuarioActual).State = EntityState.Modified;
            _reservasContexto.SaveChanges();
            return Ok(UsuarioModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            Usuarios? usuario = (from e in _reservasContexto.usuarios where e.Id == id select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }

            _reservasContexto.usuarios.Attach(usuario);
            _reservasContexto.usuarios.Remove(usuario);
            _reservasContexto.SaveChanges();

            return Ok(usuario);
        }
    }
}
