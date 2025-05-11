using HackathonHealthMed.Agendamentos.Models;
using HackathonHealthMed.Contracts;

namespace HackathonHealthMed.Agendamentos.Services.Interfaces
{
    public interface IAgendamentoService
    {
        List<Agendamento> ListarAgendamentos(Guid pacienteId);
        void CriarAgendamento(Agendamento agendamento);
        Agendamento CancelarAgendamento(Agendamento agendamento, string justificativa);
        Agendamento AtualizaStatusAgendamento(Guid agendamentoId, StatusAgendamento statusAgendamento);
        Agendamento ObterAgendamento(Guid idAgendamento);
        List<Agendamento> ListarAgendamentosPorMedico(string medicoCrm);
        List<Agendamento> ListarAgendamentosPorMedicoStatus(string medicoCrm, StatusAgendamento statusAgendamento);
    }
}
