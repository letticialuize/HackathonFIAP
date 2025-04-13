using HackathonHealthMed.Agendamentos.Data;
using HackathonHealthMed.Agendamentos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackathonHealthMed.Agendamentos.Controllers;

[ApiController]
[Route("[controller]")]
public class AgendamentoController : ControllerBase
{
    private readonly AgendamentoDbContext _context;

    public AgendamentoController(AgendamentoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Agendamento>>> RetornarAgendamentos()
    {
        return await _context.Agendamentos.ToListAsync(); 
    }

    [HttpPost]
    public async Task<ActionResult<Agendamento>> CriarAgendamentos(Agendamento agendamento)
    {
        _context.Agendamentos.Add(agendamento);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(RetornarAgendamentos), new { id = agendamento.Id }, agendamento);
    }
}
