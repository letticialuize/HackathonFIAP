using HackathonHealthMed.Agendamentos.Data;
using HackathonHealthMed.Agendamentos.DTOs;
using HackathonHealthMed.Agendamentos.Models;
using HackathonHealthMed.Agendamentos.Services;
using HackathonHealthMed.Agendamentos.Services.Interfaces;
using HackathonHealthMed.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackathonHealthMed.Agendamentos.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AgendamentoController : ControllerBase
{
    private readonly IAgendamentoService _agendamentoService;
    private readonly IAutenticacaoApiService _autenticacaoApiService;
    private readonly IHorarioApiService _horarioApiService;

    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly IBus _bus;


    public AgendamentoController(IAgendamentoService agendamentoService, IAutenticacaoApiService autenticacaoApiService,
                                 IHorarioApiService horarioApiService, ITokenService tokenService, IConfiguration configuration, IBus bus)
    {
        _agendamentoService = agendamentoService;
        _autenticacaoApiService = autenticacaoApiService;
        _horarioApiService = horarioApiService;
        _tokenService = tokenService;
        _configuration = configuration;
        _bus = bus;
    }

    [HttpGet("RetornarAgendamentos")]
    public async Task<ActionResult<IEnumerable<Agendamento>>> RetornarAgendamentos()
    {
        var paciente = _tokenService.ConverteTokenAuthorizationPaciente();
        return _agendamentoService.ListarAgendamentos(paciente.Id);
    }

    [HttpPost("CriarAgendamentos")]
    public async Task<ActionResult<Agendamento>> CriarAgendamentos(CriarAgendamentoDTO agendamentoDTO)
    {
        if (ModelState.IsValid)
        {
            var paciente = _tokenService.ConverteTokenAuthorizationPaciente();
            var horarioConsulta = await _horarioApiService.ObterHorarioPorId(agendamentoDTO.HorarioConsultaId); 
            var agendamento = new Agendamento
            {
                Id = Guid.NewGuid(),
                HorarioConsultaId = agendamentoDTO.HorarioConsultaId,
                PacienteId = paciente.Id,
                Justificativa = string.Empty,
                Status = StatusAgendamento.Pendente,
                MedicoCrm = horarioConsulta.MedicoCrm
            };


            _agendamentoService.CriarAgendamento(agendamento);

            return CreatedAtAction(nameof(RetornarAgendamentos), new { id = agendamento.Id }, agendamento);
        }

        return BadRequest("Não foi possível realizar o agendamento.");
    }

    [HttpPut("CancelarAgendamento")]
    public async Task<ActionResult<Agendamento>> CancelarAgendamento(CancelarAgendamentoDTO cancelarAgendamentoDTO)
    {
        var paciente = _tokenService.ConverteTokenAuthorizationPaciente();

        var agendamento = _agendamentoService.ObterAgendamento(cancelarAgendamentoDTO.AgendamentoId);

        if (agendamento is null)
            Conflict("Agendamento não encontrado");

        await EnviarParaFilaAsync("FilaDesocupaHorario", agendamento);

        _agendamentoService.CancelarAgendamento(agendamento, cancelarAgendamentoDTO.Justificativa);

        return Ok(agendamento);
    }

    [HttpGet("ListarMedicosPorEspecialidade")]
    public async Task<ActionResult<List<MedicoDTO>>> ListarMedicosPorEspecialidade(string especialidade)
    {
        var retorno = await _autenticacaoApiService.ListarMedicos(especialidade);
        return Ok(retorno);
    }

    [HttpGet("ListarHorariosDisponiveisPorCrm")]
    public async Task<IActionResult> ListarHorariosDisponiveisPorCrm([FromQuery] string crm)
    {
        var horariosDisponiveis = await _horarioApiService.ListarHorariosDisponiveis(crm);

        return Ok(horariosDisponiveis);
    }

    [HttpGet("ListarAgendamentosPorMedico")]
    public async Task<IActionResult> ListarAgendamentosPorMedico(string medicoCrm)
    {
        var agendamentosPorMedico = _agendamentoService.ListarAgendamentosPorMedico(medicoCrm);
        return Ok(agendamentosPorMedico);
    }

    [HttpGet("ListarAgendamentosPorMedicoPendente")]
    public async Task<IActionResult> ListarAgendamentosPorMedicoPendente(string medicoCrm)
    {
        var agendamentosPorMedico = _agendamentoService.ListarAgendamentosPorMedicoStatus(medicoCrm, StatusAgendamento.Pendente);
        return Ok(agendamentosPorMedico);
    }

    [HttpGet("ListarAgendamentosPorMedicoConfirmado")]
    public async Task<IActionResult> ListarAgendamentosPorMedicoConfirmado(string medicoCrm)
    {
        var agendamentosPorMedico = _agendamentoService.ListarAgendamentosPorMedicoStatus(medicoCrm, StatusAgendamento.Confirmado);
        return Ok(agendamentosPorMedico);
    }

    [HttpGet("ObterAgendamento")]
    public async Task<IActionResult> ObterAgendamento(Guid agendamentoId)
    {
        var agendamento = _agendamentoService.ObterAgendamento((agendamentoId));
        return Ok(agendamento);
    }
    private async Task EnviarParaFilaAsync(string nomeFila, Agendamento agendamento)
    {
        AgendamentoContract ac = new()
        {
            Id = agendamento.Id,
            HorarioConsultaId = agendamento.HorarioConsultaId,
            Justificativa = agendamento.Justificativa,
            Status = agendamento.Status,
            MedicoCrm = agendamento.MedicoCrm,
            PacienteId = agendamento.PacienteId
        };

        var configFila = _configuration[$"MassTransit:{nomeFila}"];
        if (string.IsNullOrWhiteSpace(configFila))
            throw new InvalidOperationException("Fila de agendamento não configurada.");

        var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{configFila}"));
        var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));

        await endpoint.Send(ac, cancellationTokenSource.Token);
    }

}
