using HackathonHealthMed.GestaoHorarios.DTOs;
using HackathonHealthMed.GestaoHorarios.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace HackathonHealthMed.GestaoHorarios.Services
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
        public MedicoDTO ConverteTokenAuthorizationMedico()
        {
            var claimsPorTipo = ObterClaimsTokenHeader().FirstOrDefault(x => x.Type == "medico").Value;
            var medico = JsonSerializer.Deserialize<MedicoDTO>(claimsPorTipo);
            return medico;
        }
    }
}
