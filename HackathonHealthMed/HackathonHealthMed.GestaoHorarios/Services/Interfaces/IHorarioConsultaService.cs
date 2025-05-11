using HackathonHealthMed.GestaoHorarios.DTOs;
using HackathonHealthMed.GestaoHorarios.Models;

namespace HackathonHealthMed.GestaoHorarios.Services.Interfaces
{
    public interface IHorarioConsultaService
    {
        public void AdicionarHorarioConsulta(HorarioConsulta horarioConsulta);
        public void AtualizarHorarioConsulta(HorarioConsulta horarioConsulta);
        public void ExcluirContato(Guid IdHorarioConsulta);
        public List<HorarioConsulta> ConsultarHorariosPorData(DateTime horario);
        public List<HorarioConsulta> ConsultarHorariosPorDataECrm(DateTime horario, string crm); 
        public List<HorarioConsulta> ListarHorariosConsulta();
        public HorarioConsulta ConsultaHorarioPorId(Guid horarioId);
        public bool ValidaHorarioPorMedico(DateTime horario, string crmMedico);
        public List<AgendaMedicoDTO> ConsultarHorariosDisponiveisPorCrm(string crm);
        public void OcupaHorarioDisponivel(Guid id);
        public void DesocupaHorario(Guid id);
    }
}
