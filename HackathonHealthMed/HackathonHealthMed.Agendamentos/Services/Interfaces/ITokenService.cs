using HackathonHealthMed.Autenticacao.Models.DTOs;
using System.Security.Claims;

namespace HackathonHealthMed.Agendamentos.Services.Interfaces
{
    public interface ITokenService
    {
        string ObterTokenAuthorizationHeader();
        IEnumerable<Claim> ObterClaimsTokenHeader();
        public PacienteDTO ConverteTokenAuthorizationPaciente();
    }
}
