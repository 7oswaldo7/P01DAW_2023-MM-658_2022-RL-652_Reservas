using Microsoft.EntityFrameworkCore;
using P01DAW_2023_MM_658_2022_RL_652_Reservas.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyeccion por dependencia del string de conexion al contexto
builder.Services.AddDbContext<ReservasContext>(options =>
    options.UseSqlServer(
            builder.Configuration.GetConnectionString("reservaDbConnection")
        )
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
