namespace HackathonHealthMed.GestaoConsultas.DTOs
{
    public class MedicoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CRM { get; set; }
        public string Especialidade { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
