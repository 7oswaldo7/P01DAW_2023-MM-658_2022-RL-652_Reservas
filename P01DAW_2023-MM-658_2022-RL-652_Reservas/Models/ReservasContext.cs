using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using P01DAW_2023_MM_658_2022_RL_652_Reservas.Models;
namespace WebApiPractica.Models
{
    public class ReservasContext : DbContext
    {
        public ReservasContext(DbContextOptions<ReservasContext> options) : base(options)
        {

        }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Sucursal> sucursales { get; set; }
        public DbSet<Reserva> reservas { get; set; }
        public DbSet<EspacioParqueo> espacioParqueos { get; set; }
    }
}
