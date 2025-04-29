using HackathonHealthMed.Agendamentos.Models;
using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.Agendamentos.DTOs
{
    public class CancelarAgendamentoDTO
    {
        public Guid AgendamentoId { get; set; }
        public string Justificativa { get; set; }

    }
}
