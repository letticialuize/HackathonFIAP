using HackathonHealthMed.GestaoHorarios.Models;
using HackathonHealthMed.GestaoHorarios.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackathonHealthMed.GestaoHorarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestaoHorarioController : ControllerBase
    {
        private readonly IHorarioConsultaService _horarioConsultaService;
        public GestaoHorarioController(IHorarioConsultaService horarioConsultaService)
        {
            _horarioConsultaService = horarioConsultaService;
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
        public IActionResult AdicionarHorario(DateTime horarioInicial, DateTime horarioFinal)
        {
            var horarioConsulta = new HorarioConsulta
            {
                Id = Guid.NewGuid(),
                MedicoCrm = "123456",
                HorarioInicial = horarioInicial,
                HorarioFinal = horarioFinal,
                EstaDisponivel = true
            };
            _horarioConsultaService.AdicionarHorarioConsulta(horarioConsulta);
            return CreatedAtAction(nameof(ListarHorarios), new { id = horarioConsulta.Id }, horarioConsulta);
        }
    }
}
