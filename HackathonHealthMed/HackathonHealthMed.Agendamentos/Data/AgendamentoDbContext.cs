using Microsoft.EntityFrameworkCore;
using HackathonHealthMed.Agendamentos.Models;

namespace HackathonHealthMed.Agendamentos.Data;

public class AgendamentoDbContext : DbContext 
{
    public AgendamentoDbContext(DbContextOptions<AgendamentoDbContext> options)
        : base(options) { }

    public DbSet<Agendamento> Agendamentos { get; set; }
}
