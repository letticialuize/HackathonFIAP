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
    private readonly ITokenService _tokenService;

    public AgendamentoController(IAgendamentoService agendamentoService, IAutenticacaoApiService autenticacaoApiService, ITokenService tokenService)
    {
        _agendamentoService = agendamentoService;
        _autenticacaoApiService = autenticacaoApiService;
        _tokenService = tokenService;
    }

    [HttpGet("RetornarAgendamentos")]
    public async Task<ActionResult<IEnumerable<Agendamento>>> RetornarAgendamentos()
    {
        var paciente = _tokenService.ConverteTokenAuthorizationPaciente();
        return  _agendamentoService.ListarAgendamentos(paciente.Id);
    }

    [HttpPost("CriarAgendamentos")]
    public async Task<ActionResult<Agendamento>> CriarAgendamentos(Agendamento agendamento)
    {
        _agendamentoService.CriarAgendamento(agendamento);
        return CreatedAtAction(nameof(RetornarAgendamentos), new { id = agendamento.Id }, agendamento);
    }

    [HttpGet("ListarMedicosPorEspecialidade")]
    public async Task<ActionResult<List<MedicoDTO>>> ListarMedicosPorEspecialidade(string especialidade)
    {
        var retorno = await _autenticacaoApiService.ListarMedicos(especialidade);
        return Ok(retorno);
    }
}
