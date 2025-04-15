using HackathonHealthMed.GestaoHorarios.DTOs;
using System.Security.Claims;

namespace HackathonHealthMed.GestaoHorarios.Services.Interfaces
{
    public interface ITokenService
    {
        string ObterTokenAuthorizationHeader();
        IEnumerable<Claim> ObterClaimsTokenHeader();
        public MedicoDTO ConverteTokenAuthorizationMedico();
    }
}
