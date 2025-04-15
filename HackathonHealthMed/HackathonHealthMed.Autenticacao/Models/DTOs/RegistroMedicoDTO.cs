using HackathonHealthMed.Autenticacao.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.Autenticacao.Models.DTOs
{
    public class RegistroMedicoDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CRM é obrigatório.")]
        [RegularExpression(@"^\d{4,6}/[A-Z]{2}$", ErrorMessage = "O CRM deve estar no formato '123456/ZZ'.")]
        public string CRM { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [EnumDataType(typeof(EnumEspecialidadeMedica), ErrorMessage = "Especialidade inválida.")]
        public EnumEspecialidadeMedica Especialidade { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres.")]
        public string Senha { get; set; }

    }
}
