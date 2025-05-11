using HackathonHealthMed.Agendamentos.Data;
using HackathonHealthMed.Agendamentos.Models;
using HackathonHealthMed.Agendamentos.Services.Interfaces;
using HackathonHealthMed.Contracts;
using Microsoft.AspNetCore.HttpLogging;

namespace HackathonHealthMed.Agendamentos.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly AgendamentoDbContext _context;

        public AgendamentoService(AgendamentoDbContext context)
        {
            _context = context;
        }
        public Agendamento CancelarAgendamento(Agendamento agendamento, string justificativa)
        {
            agendamento.Status = StatusAgendamento.Cancelado;
            agendamento.Justificativa = justificativa;
            _context.Agendamentos.Update(agendamento);
            _context.SaveChanges();

            return agendamento;
        }

        public Agendamento AtualizaStatusAgendamento(Guid agendamentoId, StatusAgendamento statusAgendamento)
        {
            var agendamento = _context.Agendamentos.FirstOrDefault(x => x.Id == agendamentoId);
            agendamento.Status = statusAgendamento;
            _context.Agendamentos.Update(agendamento);
            _context.SaveChanges();

            return agendamento;
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

        public Agendamento? ObterAgendamento(Guid idAgendamento)
        {
            return _context.Agendamentos
                .FirstOrDefault(x => x.Id == idAgendamento);
        }

        public List<Agendamento> ListarAgendamentosPorMedico(string crmMedico)
        {
            return _context.Agendamentos.Where(x => x.MedicoCrm == crmMedico).ToList();
        }

        public List<Agendamento> ListarAgendamentosPorMedicoStatus(string medicoCrm, StatusAgendamento statusAgendamento)
        {
            return _context.Agendamentos.Where(x => x.MedicoCrm == medicoCrm && x.Status == statusAgendamento).ToList();
        }
    }
}
