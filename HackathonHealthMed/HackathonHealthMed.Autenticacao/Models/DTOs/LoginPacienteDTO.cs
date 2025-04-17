using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.Autenticacao.Models.DTOs
{
    public class LoginPacienteDTO : IValidatableObject
    {
        public string Cpf { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Cpf) && string.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult(
                    "Você deve informar o CPF ou o E-mail.",
                    new[] { nameof(Cpf), nameof(Email) }
                );
            }
        }
    }
}
