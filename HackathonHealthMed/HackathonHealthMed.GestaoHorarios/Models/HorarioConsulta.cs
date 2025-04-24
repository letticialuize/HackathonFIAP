using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.GestaoHorarios.Models
{
    public class HorarioConsulta
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O CRM do médico é obrigatório")]
        [MaxLength(10, ErrorMessage = "O CRM deve ter no máximo 10 caracteres")]
        public string MedicoCrm { get; set; }

        [Required(ErrorMessage = "Horário Inicial é obrigatório")]
        public DateTime HorarioInicial { get; set; }

        [Required(ErrorMessage = "Horário Final é obrigatório")]

        public DateTime HorarioFinal { get; set; }

        [Required]
        public bool EstaDisponivel { get; set; }
    }
}
