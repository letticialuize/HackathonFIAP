using HackathonHealthMed.Autenticacao.Models.DTOs;
using HackathonHealthMed.Autenticacao.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using HackathonHealthMed.Autenticacao.Data;
using HackathonHealthMed.Autenticacao.Extensions;

namespace HackathonHealthMed.Autenticacao.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IAutenticacaoService _autenticacaoService;

        public JwtService(IConfiguration configuration, IAutenticacaoService autenticacaoService)
        {
            _configuration = configuration;
            _autenticacaoService = autenticacaoService;
        }
        public string GerarJwtTokenMedico(IdentityUser user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var medicoDTO = _autenticacaoService.BuscarMedicoPorId(user.Id);

            var medicoJson = JsonSerializer.Serialize(medicoDTO);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("medico", medicoJson)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GerarJwtTokenPaciente(IdentityUser user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var pacienteDTO = _autenticacaoService.BuscarPacientePorId(user.Id);

            var pacienteJson = JsonSerializer.Serialize(pacienteDTO);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("paciente", pacienteJson)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
