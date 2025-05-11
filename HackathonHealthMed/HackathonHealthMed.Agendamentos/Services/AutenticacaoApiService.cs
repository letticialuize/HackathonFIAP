using HackathonHealthMed.Agendamentos.DTOs;
using HackathonHealthMed.Agendamentos.Services.Interfaces;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HackathonHealthMed.Agendamentos.Services
{
    public class AutenticacaoApiService : IAutenticacaoApiService
    {
        private readonly ITokenService _tokenService;
        public AutenticacaoApiService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public async Task<List<MedicoDTO>> ListarMedicos(string especialidade)
        {
            var token = _tokenService.ObterTokenAuthorizationHeader();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:31305/api/Medico/listar-medicos-por-especialidade?especialidade={especialidade}");
            var response = new List<MedicoDTO>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
                var responseAutenticacaoApi = await client.SendAsync(request);
                var contentResp = await responseAutenticacaoApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<List<MedicoDTO>>(contentResp, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return objResponse;

            }
        }
    }
}
