using HackathonHealthMed.Autenticacao.Models.DTOs;
using HackathonHealthMed.Autenticacao.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace HackathonHealthMed.Autenticacao.Services.Interfaces
{
    public interface IJwtService
    {
        string GerarJwtTokenMedico(IdentityUser user);
        string GerarJwtTokenPaciente(IdentityUser user);

    }
}
