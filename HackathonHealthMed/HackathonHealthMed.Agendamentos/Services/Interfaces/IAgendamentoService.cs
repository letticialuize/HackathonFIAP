using HackathonHealthMed.Agendamentos.Models;

namespace HackathonHealthMed.Agendamentos.Services.Interfaces
{
    public interface IAgendamentoService
    {
        public List<Agendamento> ListarAgendamentos(Guid pacienteId);
        public void CriarAgendamento(Agendamento agendamento);
        public void CancelarAgendamento(Agendamento agendamento, string justificativa);
    }
}
