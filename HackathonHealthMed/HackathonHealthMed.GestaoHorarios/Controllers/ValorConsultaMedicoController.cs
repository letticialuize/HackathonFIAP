using HackathonHealthMed.GestaoHorarios.Models;
using HackathonHealthMed.GestaoHorarios.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackathonHealthMed.GestaoHorarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValorConsultaMedicoController : ControllerBase
    {
        private readonly IValorConsultaMedicoService _valorConsultaMedicoService;
        private readonly ITokenService _tokenService;

        public ValorConsultaMedicoController(ITokenService tokenService, IValorConsultaMedicoService valorConsultaMedicoService)
        {
            _tokenService = tokenService;
            _valorConsultaMedicoService = valorConsultaMedicoService;
        }

        [HttpGet]
        public IActionResult ListarValorConsulta()
        {
            var medico = _tokenService.ConverteTokenAuthorizationMedico();
            var valorConsulta = _valorConsultaMedicoService.ObterValorConsulta(medico.CRM);

            return Ok(new { ValorConsulta = valorConsulta });
        }

        [HttpPost("AtualizaValorConsulta")]
        public IActionResult AtualizaValorConsulta(decimal novoValor)
        {
            var medico = _tokenService.ConverteTokenAuthorizationMedico();
            var valorConsultaMedico = _valorConsultaMedicoService.ObterValorConsulta(medico.CRM);
            if (valorConsultaMedico == null)
            {
                return NotFound(new { Mensagem = "Valor da consulta não encontrado." });
            }
            valorConsultaMedico.Valor = novoValor;
            _valorConsultaMedicoService.AtualizarValorConsulta(valorConsultaMedico);

            return CreatedAtAction("AtualizaValorConsulta", valorConsultaMedico);
        }

        [HttpPost("AdicionaValorConsulta")]
        public IActionResult AdicionaValorConsulta(decimal valor)
        {
            var medico = _tokenService.ConverteTokenAuthorizationMedico();
            ValorConsultaMedico valorConsultaMedico = new()
            {
                Valor = valor,
                MedicoCrm = medico.CRM
            };
            var valorConsultaExistente = _valorConsultaMedicoService.ValidaExistenciaConsultaPorMedico(medico.CRM);
            if (valorConsultaExistente)
            {
                return Conflict(new { Mensagem = "Valor da consulta já cadastrado." });
            }
            _valorConsultaMedicoService.AdicionaValorConsulta(valorConsultaMedico);

            return CreatedAtAction("AdicionaValorConsulta", valorConsultaMedico);
        }
    }
}
