using HackathonHealthMed.Agendamentos.Models;

namespace HackathonHealthMed.Agendamentos.Services.Interfaces
{
    public interface IAgendamentoService
    {
        List<Agendamento> ListarAgendamentos(Guid pacienteId);
        void CriarAgendamento(Agendamento agendamento);
        Agendamento CancelarAgendamento(Agendamento agendamento, string justificativa);
        Agendamento ObterAgendamento(Guid idAgendamento);
    }
}
