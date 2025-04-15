using HackathonHealthMed.Autenticacao.Data;
using HackathonHealthMed.Autenticacao.Models.DTOs;
using HackathonHealthMed.Autenticacao.Models.Entities;
using HackathonHealthMed.Autenticacao.Models.Enums;
using HackathonHealthMed.Autenticacao.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace HackathonHealthMed.Autenticacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(UserManager<IdentityUser> userManager, IConfiguration configuration, AutenticacaoDbContext context, 
            IAutenticacaoService autenticacaoService, IJwtService jwtService)
        {
            _userManager = userManager;
            _autenticacaoService=autenticacaoService;
            _jwtService = jwtService;
        }

        [HttpPost("registrar-paciente")]
        public async Task<IActionResult> CriarPaciente([FromBody] RegistroPacienteDTO pacienteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = pacienteDTO.Email,
                Email = pacienteDTO.Email,
            };

            var result = await _userManager.CreateAsync(user, pacienteDTO.Senha);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, EnumRole.PACIENTE.ToString());

            _autenticacaoService.AdicionarPaciente(pacienteDTO, user);

            return Ok(new { message = "Paciente criado com sucesso!" });
        }

        [HttpPost("registrar-medico")]
        public async Task<IActionResult> CriarMedico([FromBody] RegistroMedicoDTO medicoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = medicoDTO.Email,
                Email = medicoDTO.Email,
            };

            var result = await _userManager.CreateAsync(user, medicoDTO.Senha);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, EnumRole.MEDICO.ToString());

            _autenticacaoService.AdicionarMedico(medicoDTO, user);

            return Ok(new { message = "Médico criado com sucesso!" });
        }

        [HttpPost("login-medico")]
        public async Task<IActionResult> LoginMedico([FromBody] LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Senha))
                return Unauthorized(new { message = "Credenciais inválidas" });

            var token = _jwtService.GerarJwtTokenMedico(user);

            return Ok($"Bearer {token}");
        }

        [HttpPost("login-paciente")]
        public async Task<IActionResult> LoginPaciente([FromBody] LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Senha))
                return Unauthorized(new { message = "Credenciais inválidas" });

            var token = _jwtService.GerarJwtTokenPaciente(user);

            return Ok($"Bearer {token}");
        }


    }
}
