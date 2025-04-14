using HackathonHealthMed.GestaoHorarios.Models;

namespace HackathonHealthMed.GestaoHorarios.Services.Interfaces
{
    public interface IHorarioConsultaService
    {
        public void AdicionarHorarioConsulta(HorarioConsulta horarioConsulta);
        public void AtualizarHorarioConsulta(HorarioConsulta horarioConsulta);
        public void ExcluirContato(Guid IdHorarioConsulta);
        public List<HorarioConsulta> ConsultarHorariosPorData(DateTime horario);
        public List<HorarioConsulta> ListarHorariosConsulta();
    }
}
