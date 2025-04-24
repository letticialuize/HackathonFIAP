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
using HackathonHealthMed.Autenticacao.Models.Enums;

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

        public ResponseMedicoDTO BuscarMedicoPorCRM(string crm)
        {
            var medico = _context.Medico.FirstOrDefault(x => x.CRM == crm.ToString());

            if (medico == null)
                throw new Exception("Médico não encontrado.");

            return new ResponseMedicoDTO()
            {
                Id = medico.Id,
                Nome = medico.Nome,
                CRM = medico.CRM,
                Especialidade = medico.Especialidade.GetDisplayName(),
                UsuarioId = medico.UsuarioId
            };
        }

        public List<ResponseMedicoDTO> BuscarMedicosPorEspecialidade(string especialidade)
        {
            var enumEspecialidade = EnumExtensions.GetEnumByDisplayName<EnumEspecialidadeMedica>(especialidade);

            if (!enumEspecialidade.HasValue)
                throw new Exception("Especialidade não encontrada.");

            var medicos = _context.Medico.Where(x => x.Especialidade == enumEspecialidade)
                                          .Select(medico => new ResponseMedicoDTO()
                                          {
                                              Id = medico.Id,
                                              Nome = medico.Nome,
                                              CRM = medico.CRM,
                                              Especialidade = medico.Especialidade.GetDisplayName(),
                                              UsuarioId = medico.UsuarioId
                                          }).ToList();

            return medicos;
        }

        public ResponsePacienteDTO BuscarPacientePorCPF(string cpf)
        {
            var paciente = _context.Paciente.FirstOrDefault(x => x.CPF == cpf);

            if (paciente == null)
                throw new Exception("Paciente não encontrado.");

            return new ResponsePacienteDTO()
            {
                Id = paciente.Id,
                Nome = paciente.Nome,
                CPF = paciente.CPF,
                UsuarioId = paciente.UsuarioId
            };
        }
    }
}
