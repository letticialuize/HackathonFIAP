using HackathonHealthMed.GestaoHorarios.DTOs;
using HackathonHealthMed.GestaoHorarios.Models;
using HackathonHealthMed.GestaoHorarios.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using static System.Net.Mime.MediaTypeNames;

namespace HackathonHealthMed.GestaoHorarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GestaoHorarioController : ControllerBase
    {
        private readonly IHorarioConsultaService _horarioConsultaService;
        private readonly ITokenService _tokenService;

        public GestaoHorarioController(IHorarioConsultaService horarioConsultaService, ITokenService tokenService)
        {
            _horarioConsultaService = horarioConsultaService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult ListarHorarios()
        {
            return Ok(_horarioConsultaService.ListarHorariosConsulta());
        }

        [HttpGet("ListarHorariosPorData")]
        public IActionResult ListarHorariosPorData(DateTime data)
        {
            return Ok(_horarioConsultaService.ConsultarHorariosPorData(data));
        }

        [HttpGet("ListarHorariosPorDataECrm")]
        public IActionResult ListarHorariosPorDataECrm(DateTime data,[FromQuery]string crm)
        {
            return Ok(_horarioConsultaService.ConsultarHorariosPorDataECrm(data, crm));
        }

        [HttpPost]
        public IActionResult AdicionarHorario(DateTime horarioInicial  )
        {
            var medico = _tokenService.ConverteTokenAuthorizationMedico();
            if (_horarioConsultaService.ValidaHorarioPorMedico(horarioInicial, medico.CRM))
                return Conflict(new { Mensagem = "Horário já cadastrado" });

            var horarioConsulta = new HorarioConsulta
            {
                Id = Guid.NewGuid(),
                MedicoCrm = medico.CRM,
                HorarioInicial = horarioInicial,
                HorarioFinal = horarioInicial.AddHours(1),
                EstaDisponivel = true
            };
            _horarioConsultaService.AdicionarHorarioConsulta(horarioConsulta);
            return CreatedAtAction(nameof(ListarHorarios), new { id = horarioConsulta.Id }, horarioConsulta);

        }

        [HttpPut("{id}")]
        public IActionResult AtualizarHorario(Guid id, DateTime horarioInicial)
        {
            var horarioSemAlteracao = _horarioConsultaService.ListarHorariosConsulta().FirstOrDefault(h => h.Id == id);
            if(!horarioSemAlteracao.EstaDisponivel)
                return Conflict(new { Mensagem = "Não é possível alterar horário com agendamento confirmado." });
            var medico = _tokenService.ConverteTokenAuthorizationMedico();
            if (_horarioConsultaService.ValidaHorarioPorMedico(horarioInicial, medico.CRM))
                return Conflict(new { Mensagem = "Horário já cadastrado." });
            horarioSemAlteracao.HorarioInicial = horarioInicial;
            horarioSemAlteracao.HorarioFinal = horarioInicial.AddHours(1);
            _horarioConsultaService.AtualizarHorarioConsulta(horarioSemAlteracao);
            return Ok(horarioSemAlteracao);
        }
    }
}
