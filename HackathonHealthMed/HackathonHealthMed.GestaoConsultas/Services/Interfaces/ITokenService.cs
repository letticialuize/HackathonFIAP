using HackathonHealthMed.GestaoConsultas.DTOs;
using System.Security.Claims;

namespace HackathonHealthMed.GestaoConsultas.Services.Interfaces
{
    public interface ITokenService
    {
        string ObterTokenAuthorizationHeader();
        IEnumerable<Claim> ObterClaimsTokenHeader();
        public MedicoDTO ConverteTokenAuthorizationMedico();
    }
}
