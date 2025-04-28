using HackathonHealthMed.Agendamentos.DTOs;
using HackathonHealthMed.Agendamentos.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HackathonHealthMed.Agendamentos.Services
{
    public class HorarioApiService : IHorarioApiService
    {
        private readonly ITokenService _tokenService;

        public HorarioApiService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<List<HorarioDisponivelDTO>> ListarHorariosDisponiveis(string crm)
        {
            var token = _tokenService.ObterTokenAuthorizationHeader();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5100/api/GestaoHorario/ListarHorariosDisponiveisPorCrm?crm={crm}");
            
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseAutenticacaoApi = await client.SendAsync(request);
                var contentResp = await responseAutenticacaoApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<List<HorarioDisponivelDTO>>(contentResp, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return objResponse;
            }
        }
    }
}