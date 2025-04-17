using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.Autenticacao.Models.DTOs
{
    public class LoginMedicoDTO
    {
        [Required(ErrorMessage = "O CRM é obrigatório.")]
        public string CRM { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}
