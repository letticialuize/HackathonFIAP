using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.GestaoConsultas.Models
{
    public class HorarioConsulta
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O CRM do médico é obrigatório")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "O CRM deve ser um número de 4 a 6 dígitos.")]
        public string MedicoId { get; set; }

        [Required(ErrorMessage = "Horário Inicial é obrigatório")]
        public DateTime HorarioInicial { get; set; }

        [Required(ErrorMessage = "Horário Final é obrigatório")]

        public DateTime HorarioFinal { get; set; }

        [Required]
        public bool EstaDisponivel { get; set; }
    }
}
