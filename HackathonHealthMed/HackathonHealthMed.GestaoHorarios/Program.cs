using HackathonHealthMed.GestaoHorarios.Data;
using HackathonHealthMed.GestaoHorarios.Services;
using HackathonHealthMed.GestaoHorarios.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GestaoHorarioDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        opt => opt.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds));
});
builder.Services.AddSingleton<IHorarioConsultaService, HorarioConsultaService>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
