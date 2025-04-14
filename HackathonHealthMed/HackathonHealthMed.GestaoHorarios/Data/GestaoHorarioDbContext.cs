using HackathonHealthMed.GestaoHorarios.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HackathonHealthMed.GestaoHorarios.Data
{
    public class GestaoHorarioDbContext : DbContext
    {
        public GestaoHorarioDbContext(DbContextOptions<GestaoHorarioDbContext> options)
        : base(options) { }

        public DbSet<HorarioConsulta> HorarioConsulta { get; set; }
    }
}
