using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HackathonHealthMed.Autenticacao.Models.Entities;

namespace HackathonHealthMed.Autenticacao.Data
{
    public class AutenticacaoDbContext : IdentityDbContext<IdentityUser>
    {
        public AutenticacaoDbContext(DbContextOptions<AutenticacaoDbContext> options)
            : base(options)
        {

        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Medico>()
                .HasOne(m => m.Usuario)
                .WithMany()
                .HasForeignKey(m => m.UsuarioId);

            builder.Entity<Paciente>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Paciente", NormalizedName = "PACIENTE" },
                new IdentityRole { Name = "Médico", NormalizedName = "MÉDICO" }
            );
        }
    }
}
