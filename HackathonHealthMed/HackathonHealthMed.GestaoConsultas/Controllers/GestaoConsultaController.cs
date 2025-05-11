using HackathonHealthMed.Contracts;
using HackathonHealthMed.GestaoConsultas.DTOs;
using HackathonHealthMed.GestaoConsultas.Services.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace HackathonHealthMed.GestaoConsultas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GestaoConsultaController : ControllerBase
    {
        private readonly IAgendamentoApiService _agendamentoApiService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IBus _bus;

        public GestaoConsultaController(ITokenService tokenService,
            IConfiguration configuration, IBus bus, IAgendamentoApiService agendamentoApiService)
        {
            _tokenService = tokenService;
            _configuration = configuration;
            _bus = bus;
            _agendamentoApiService = agendamentoApiService;

        }

        [HttpPost("ConfirmaConsulta")]
        public async Task<IActionResult> ConfirmaConsulta(Guid agendamentoId)
        {
            var agendamento = await _agendamentoApiService.ObterAgendamento(agendamentoId);
            if (agendamento == null)
                return NotFound("Agendamento não existe");

            if (agendamento.Status != StatusAgendamento.Pendente)
            {
                return BadRequest("Não é possível confirmar agendamento que não está pendente");
            }
            var agendamentoContract = new AgendamentoContract()
            {
                Id = agendamento.Id,
                HorarioConsultaId = agendamento.HorarioConsultaId,
                MedicoCrm = agendamento.MedicoCrm,
                PacienteId = agendamento.PacienteId,
                Status = StatusAgendamento.Confirmado
            };
            await EnviarParaFilaAsync("FilaOcupaHorario", agendamentoContract);
            await EnviarParaFilaAsync("FilaAtualizaStatusConsulta", agendamentoContract);
            return Ok();
        }

        [HttpPost("RecusaConsulta")]
        public async Task<IActionResult> RecusaConsulta(Guid agendamentoId)
        {
            var agendamento = await _agendamentoApiService.ObterAgendamento(agendamentoId);
            if (agendamento == null)
                return NotFound("Agendamento não existe");

            if (agendamento.Status != StatusAgendamento.Pendente)
            {
                return BadRequest("Não é possível recusar agendamento que não está pendente");
            }
            var agendamentoContract = new AgendamentoContract()
            {
                Id = agendamento.Id,
                HorarioConsultaId = agendamento.Id,
                MedicoCrm = agendamento.MedicoCrm,
                PacienteId = agendamento.PacienteId,
                Status = StatusAgendamento.Recusado
            };
            await EnviarParaFilaAsync("FilaDesocupaHorario", agendamentoContract);
            await EnviarParaFilaAsync("FilaAtualizaStatusConsulta", agendamentoContract);
            return Ok();
        }

        private async Task EnviarParaFilaAsync(string nomeFila, AgendamentoContract agendamentoContract)
        {
            var configFila = _configuration[$"MassTransit:{nomeFila}"];
            if (string.IsNullOrWhiteSpace(configFila))
                throw new InvalidOperationException("Fila de agendamento não configurada.");

            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{configFila}"));
            await endpoint.Send(agendamentoContract);
        }
    }
}
