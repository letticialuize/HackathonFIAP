using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.Agendamentos.Models;

public class Agendamento
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O CRM do médico é obrigatório")]
    [RegularExpression(@"^\d{4,6}$", ErrorMessage = "O CRM deve ser um número de 4 a 6 dígitos.")]
    public string MedicoId { get; set; }

    [Required(ErrorMessage = "O CPF do paciente é obrigatório")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve ter 11 dígitos.")]
    public string PacienteId { get; set; }

    [Required]
    public DateTime AgendamentoData { get; set; }

    [Required]
    public StatusAgendamento Status { get; set; }  
}

public enum StatusAgendamento
{
    Confirmado,
    Cancelado,
    Pendente
}
