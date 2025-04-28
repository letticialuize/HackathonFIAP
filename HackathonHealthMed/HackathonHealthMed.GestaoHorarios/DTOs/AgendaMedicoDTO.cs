using System.ComponentModel.DataAnnotations;

namespace HackathonHealthMed.GestaoHorarios.DTOs
{
    public class AgendaMedicoDTO
    {
        public Guid Id { get; set; }
        public string MedicoCrm { get; set; }
        public decimal ValorConsulta { get; set; }
        public DateTime HorarioInicial { get; set; }
        public DateTime HorarioFinal { get; set; }
        public bool EstaDisponivel { get; set; }
    }
}
