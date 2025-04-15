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
using HackathonHealthMed.Autenticacao.Models.Entities;

namespace HackathonHealthMed.Autenticacao.Services.Implementations
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly AutenticacaoDbContext _context;

        public AutenticacaoService(AutenticacaoDbContext context, IConfiguration configuration)
        {
            _context = context;
        }

        public void AdicionarMedico(RegistroMedicoDTO medicoDTO, IdentityUser user)
        {
            var medico = new Medico
            {
                Id = Guid.NewGuid(),
                Nome = medicoDTO.Nome,
                CRM = medicoDTO.CRM,
                Especialidade = medicoDTO.Especialidade,
                UsuarioId = user.Id,
                Usuario = user
            };

            _context.Medico.Add(medico);
            _context.SaveChanges();
        }

        public void AdicionarPaciente(RegistroPacienteDTO pacienteDTO, IdentityUser user)
        {
            var paciente = new Paciente
            {
                Id = Guid.NewGuid(),
                Nome = pacienteDTO.Nome,
                CPF = pacienteDTO.CPF,
                UsuarioId = user.Id,
                Usuario = user
            };

            _context.Paciente.Add(paciente);

            _context.SaveChanges();
        }

        public ResponseMedicoDTO BuscarMedicoPorId(string userId)
        {
            var medico = _context.Medico.FirstOrDefault(x => x.UsuarioId == userId);

            return new ResponseMedicoDTO()
            {
                Id = medico.Id,
                Nome = medico.Nome,
                CRM = medico.CRM,
                Especialidade = medico.Especialidade.GetDisplayName(),
                UsuarioId = userId
            };
        }

        public ResponsePacienteDTO BuscarPacientePorId(string userId)
        {
            var paciente = _context.Paciente.FirstOrDefault(x => x.UsuarioId == userId);

            return new ResponsePacienteDTO()
            {
                Id = paciente.Id,
                Nome = paciente.Nome,
                CPF = paciente.CPF,
                UsuarioId = userId
            };
        }
    }
}
