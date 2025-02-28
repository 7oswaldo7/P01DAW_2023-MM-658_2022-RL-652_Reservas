using System.ComponentModel.DataAnnotations;
namespace P01DAW_2023_MM_658_2022_RL_652_Reservas.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
    }

    public class Sucursales
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int AdministradorId { get; set; }
        public int EspaciosTotales { get; set; }

    }

    public class EspacioParqueos
    {
        [Key]
        public int Id { get; set; }
        public int SucursalId { get; set; }
        public int Numero { get; set; }
        public string Ubicacion { get; set; }
        public decimal CostoPorHora { get; set; }
        public string Estado { get; set; }
    }

    public class Reservas
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EspacioParqueoId { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public int HorasReservadas { get; set; }
        public string Estado { get; set; }
    }

}
