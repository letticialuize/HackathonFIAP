using HackathonHealthMed.Autenticacao.Models.Enums;

namespace HackathonHealthMed.Autenticacao.Models.DTOs
{
    public class RegistroMedicoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CRM { get; set; }
        public EnumEspecialidadeMedica Especialidade { get; set; }
    }
}
