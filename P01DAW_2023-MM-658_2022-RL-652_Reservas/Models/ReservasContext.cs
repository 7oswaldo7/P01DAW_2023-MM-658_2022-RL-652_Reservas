using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace P01DAW_2023_MM_658_2022_RL_652_Reservas.Models
{
    public class ReservasContext : DbContext
    {
        public ReservasContext(DbContextOptions<ReservasContext> options) : base(options)
        {

        }
        public DbSet<Usuarios> usuarios { get; set; }
        public DbSet<Sucursales> sucursales { get; set; }
        public DbSet<Reservas> reservas { get; set; }
        public DbSet<EspacioParqueos> espacioParqueos { get; set; }
    }
}
