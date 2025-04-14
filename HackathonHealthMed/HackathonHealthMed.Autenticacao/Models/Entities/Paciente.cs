using Microsoft.AspNetCore.Identity;

namespace HackathonHealthMed.Autenticacao.Models.Entities
{
    public class Paciente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
    }
}
