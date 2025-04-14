namespace HackathonHealthMed.Autenticacao.Models.DTOs
{
    public class RegistroPacienteDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
