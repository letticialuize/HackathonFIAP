using HackathonHealthMed.Agendamentos.DTOs;

namespace HackathonHealthMed.Agendamentos.Services.Interfaces
{
    public interface IAutenticacaoApiService
    {
        Task<List<MedicoDTO>> ListarMedicos(string especialidade);
    }
}
