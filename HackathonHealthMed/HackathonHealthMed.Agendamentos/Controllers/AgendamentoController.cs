using HackathonHealthMed.Agendamentos.Data;
using HackathonHealthMed.Agendamentos.DTOs;
using HackathonHealthMed.Agendamentos.Models;
using HackathonHealthMed.Agendamentos.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackathonHealthMed.Agendamentos.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AgendamentoController : ControllerBase
{
    private readonly IAgendamentoService _agendamentoService;
    private readonly IAutenticacaoApiService _autenticacaoApiService;
    private readonly IHorarioApiService _horarioApiService;

    private readonly ITokenService _tokenService;

    public AgendamentoController(IAgendamentoService agendamentoService, IAutenticacaoApiService autenticacaoApiService,
                                 IHorarioApiService horarioApiService, ITokenService tokenService)
    {
        _agendamentoService = agendamentoService;
        _autenticacaoApiService = autenticacaoApiService;
        _horarioApiService = horarioApiService;
        _tokenService = tokenService;
    }

    [HttpGet("RetornarAgendamentos")]
    public async Task<ActionResult<IEnumerable<Agendamento>>> RetornarAgendamentos()
    {
        var paciente = _tokenService.ConverteTokenAuthorizationPaciente();
        return  _agendamentoService.ListarAgendamentos(paciente.Id);
    }

    [HttpPost("CriarAgendamentos")]
    public async Task<ActionResult<Agendamento>> CriarAgendamentos(CriarAgendamentoDTO agendamentoDTO)
    {
        var paciente = _tokenService.ConverteTokenAuthorizationPaciente();

        var agendamento = new Agendamento
        {
            Id = Guid.NewGuid(),
            HorarioConsultaId = agendamentoDTO.HorarioConsultaId,
            PacienteId = paciente.Id,
            Justificativa = string.Empty,
            Status = StatusAgendamento.Pendente
        };

        var horarioAgendado = await _horarioApiService.OcupaHorarioDisponivel(agendamentoDTO.HorarioConsultaId);

        if (horarioAgendado != null)
        {
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
}
