using HackathonHealthMed.Autenticacao.Data;
using HackathonHealthMed.Autenticacao.Models.DTOs;
using HackathonHealthMed.Autenticacao.Models.Enums;
using HackathonHealthMed.Autenticacao.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HackathonHealthMed.Autenticacao.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MedicoController : Controller
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public MedicoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService=autenticacaoService;
        }

        [HttpGet("listar-medicos-por-especialidade")]
        public async Task<IActionResult> ListarMedicosByEspecialidade(string especialidade)
        {
            try
            {
                var medicos = _autenticacaoService.BuscarMedicosPorEspecialidade(especialidade);

                return Ok(medicos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno no servidor: {ex.Message}" });
            }
        }
    }
}
