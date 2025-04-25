using HackathonHealthMed.Agendamentos.Data;
using HackathonHealthMed.Agendamentos.Models;
using HackathonHealthMed.Agendamentos.Services.Interfaces;

namespace HackathonHealthMed.Agendamentos.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly AgendamentoDbContext _context;

        public AgendamentoService(AgendamentoDbContext context)
        {
            _context = context;
        }
        public void CancelarAgendamento(Agendamento agendamento, string justificativa)
        {
            agendamento.Status = StatusAgendamento.Cancelado;
            agendamento.Justificativa = justificativa;
            _context.Agendamentos.Update(agendamento);
            _context.SaveChanges();
        }

        public void CriarAgendamento(Agendamento agendamento)
        {
            _context.Agendamentos.Add(agendamento);
            _context.SaveChanges();
        }

        public List<Agendamento> ListarAgendamentos(Guid pacienteId)
        {
            return _context.Agendamentos
                .Where(a => a.PacienteId == pacienteId)
                .ToList();
        }
    }
}
