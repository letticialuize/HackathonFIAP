using HackathonHealthMed.Autenticacao.Models.Enums;

namespace HackathonHealthMed.Autenticacao.Models.DTOs
{
    public class ResponseMedicoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CRM { get; set; }
        public string Especialidade { get; set; }
        public string UsuarioId { get; set; }
    }
}
