using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.GestaoHorarios.Models
{
    public class ValorConsultaMedico
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O CRM do médico é obrigatório")]
        [MaxLength(10, ErrorMessage = "O CRM deve ter no máximo 10 caracteres")]
        public string MedicoCrm { get; set; }

        [Required(ErrorMessage = "Valor da Consulta é obrigatório")]
        public decimal Valor { get; set; }
    }
}
