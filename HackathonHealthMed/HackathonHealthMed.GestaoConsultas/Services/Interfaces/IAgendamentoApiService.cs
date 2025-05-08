using HackathonHealthMed.GestaoConsultas.DTOs;

namespace HackathonHealthMed.GestaoConsultas.Services.Interfaces
{
    public interface IAgendamentoApiService
    {
        Task<List<AgendamentoDTO>> ListaAgendamentosConfirmados(Guid medicoId);
        Task<List<AgendamentoDTO>> ListaAgendamentosPendentes(Guid medicoId);
    }
}
