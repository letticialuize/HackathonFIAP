using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.GestaoHorarios.Models
{
    public class HorarioConsulta
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O CRM do médico é obrigatório")]
        [MaxLength(6, ErrorMessage = "O CRM deve ter no máximo 6 dígitos.")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "O CRM deve ser um número de 4 a 6 dígitos.")]
        public string MedicoCrm { get; set; }

        [Required(ErrorMessage = "Horário Inicial é obrigatório")]
        public DateTime HorarioInicial { get; set; }

        [Required(ErrorMessage = "Horário Final é obrigatório")]

        public DateTime HorarioFinal { get; set; }

        [Required]
        public bool EstaDisponivel { get; set; }
    }
}
