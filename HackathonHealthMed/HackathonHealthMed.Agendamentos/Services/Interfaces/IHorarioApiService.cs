using HackathonHealthMed.Agendamentos.DTOs;

namespace HackathonHealthMed.Agendamentos.Services.Interfaces
{
    public interface IHorarioApiService
    {
        Task<List<HorarioDisponivelDTO>> ListarHorariosDisponiveis(string crm);
        Task<HorarioDisponivelDTO> OcupaHorarioDisponivel(Guid idHorarioConsulta);
    }
}
