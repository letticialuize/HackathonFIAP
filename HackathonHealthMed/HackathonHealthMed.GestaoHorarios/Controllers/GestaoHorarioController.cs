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

        [HttpGet("{data}")]
        public IActionResult ListarHorariosPorData(DateTime data)
        {
            return Ok(_horarioConsultaService.ConsultarHorariosPorData(data));
        }

        [HttpPost]
        public IActionResult AdicionarHorario(DateTime horarioInicial)
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
    }
}
