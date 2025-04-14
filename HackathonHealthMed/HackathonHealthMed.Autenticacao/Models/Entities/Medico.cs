using HackathonHealthMed.Autenticacao.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace HackathonHealthMed.Autenticacao.Models.Entities
{
    public class Medico
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CRM { get; set; }
        public EnumEspecialidadeMedica Especialidade { get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
    }
}
