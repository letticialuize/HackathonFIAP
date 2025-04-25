using HackathonHealthMed.Agendamentos.Services.Interfaces;
using HackathonHealthMed.Autenticacao.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace HackathonHealthMed.Agendamentos.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<Claim> ObterClaimsTokenHeader()
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(ObterTokenAuthorizationHeader());

            // Obter claims
            return jwtToken.Claims;
        }

        public string ObterTokenAuthorizationHeader()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                return authorizationHeader.ToString().Replace("Bearer ", "");
            }

            return null;

        }
        public PacienteDTO ConverteTokenAuthorizationPaciente()
        {
            var claimsPorTipo = ObterClaimsTokenHeader().FirstOrDefault(x => x.Type == "paciente").Value;
            var paciente = JsonSerializer.Deserialize<PacienteDTO>(claimsPorTipo);
            return paciente;
        }
    }
}
