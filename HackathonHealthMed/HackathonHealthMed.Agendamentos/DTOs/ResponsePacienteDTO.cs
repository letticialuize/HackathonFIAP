namespace HackathonHealthMed.Autenticacao.Models.DTOs
{
    public class PacienteDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string UsuarioId { get; set; }
    }
}
