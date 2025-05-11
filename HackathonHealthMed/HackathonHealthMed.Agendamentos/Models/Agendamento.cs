using HackathonHealthMed.Contracts;
using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.Agendamentos.Models;

public class Agendamento
{
    [Required]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O ID do paciente é obrigatório")]
    public Guid PacienteId { get; set; }

    [Required(ErrorMessage = "O ID do Horário da Consulta é obrigatório")]
    public Guid HorarioConsultaId { get; set; }

    [Required]
    public StatusAgendamento Status { get; set; }

    [MaxLength(200)]
    public string? Justificativa { get; set; }

    [Required]
    [MaxLength(20)]
    public string MedicoCrm { get; set; }
}

