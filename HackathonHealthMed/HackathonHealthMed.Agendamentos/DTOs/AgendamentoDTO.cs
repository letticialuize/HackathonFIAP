using HackathonHealthMed.Agendamentos.Models;
using HackathonHealthMed.Contracts;
using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.Agendamentos.DTOs
{
    public class AgendamentoDTO
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }

        public Guid HorarioConsultaId { get; set; }

        public StatusAgendamento Status { get; set; }

        public string? Justificativa { get; set; }
        public string MedicoCrm { get; set; }
    }
}
