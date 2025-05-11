using HackathonHealthMed.GestaoConsultas.DTOs;
using HackathonHealthMed.GestaoConsultas.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HackathonHealthMed.GestaoConsultas.Services
{
    public class AgendamentoApiService : IAgendamentoApiService
    {
        private readonly ITokenService _tokenService;
        public AgendamentoApiService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<List<AgendamentoDTO>> ListaAgendamentosConfirmados(Guid medicoId)
        {
            var token = _tokenService.ObterTokenAuthorizationHeader();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5124/api/Agendamentos/ListarAgendamentosPorMedicoConfirmado?medicoId={medicoId}");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseAutenticacaoApi = await client.SendAsync(request);
                var contentResp = await responseAutenticacaoApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<List<AgendamentoDTO>>(contentResp, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return objResponse;
            }
        }

        public async Task<List<AgendamentoDTO>> ListaAgendamentosPendentes(Guid medicoId)
        {
            var token = _tokenService.ObterTokenAuthorizationHeader();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5124/api/Agendamentos/ListarAgendamentosPorMedicoPendente?medicoId={medicoId}");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseAutenticacaoApi = await client.SendAsync(request);
                var contentResp = await responseAutenticacaoApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<List<AgendamentoDTO>>(contentResp, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return objResponse;
            }
        }
    }
}
