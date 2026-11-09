using Microsoft.EntityFrameworkCore;
using TrabajoPracticoIntegrador___Programación_2.Models;
using TrabajoPracticoIntegrador___Programación_2.Repositories;
using TrabajoPracticoIntegrador___Programación_2.Repositories.Interfaces;
using TrabajoPracticoIntegrador___Programación_2.Services;
using TrabajoPracticoIntegrador___Programación_2.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CecchettoLogicaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString
("DefaultConnection"))); // Inyección de dependencia del contexto de Base de datos a la aplicación.

builder.Services.AddScoped<IServiciosContratadosRepository, ServiciosContratadosRepository>();
builder.Services.AddScoped<IServiciosContratadosService, ServiciosContratadosService>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
