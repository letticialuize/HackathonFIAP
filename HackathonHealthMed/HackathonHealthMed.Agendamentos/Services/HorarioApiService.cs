using HackathonHealthMed.Agendamentos.DTOs;
using HackathonHealthMed.Agendamentos.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
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

        public async Task<HorarioDisponivelDTO> OcupaHorarioDisponivel(Guid idHorarioConsulta)
        {
            var token = _tokenService.ObterTokenAuthorizationHeader();
            var request = new HttpRequestMessage(HttpMethod.Put, $"http://localhost:5100/api/GestaoHorario/OcupaHorarioDisponivel?id={idHorarioConsulta}");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseAutenticacaoApi = await client.SendAsync(request);
                var contentResp = await responseAutenticacaoApi.Content.ReadAsStringAsync();

                if (responseAutenticacaoApi.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new Exception(contentResp);
                }

                responseAutenticacaoApi.EnsureSuccessStatusCode(); 

                var objResponse = JsonSerializer.Deserialize<HorarioDisponivelDTO>(contentResp, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return objResponse;
            }
        }
    }
}