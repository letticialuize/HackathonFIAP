using HackathonHealthMed.GestaoHorarios.Data;
using HackathonHealthMed.GestaoHorarios.Models;
using HackathonHealthMed.GestaoHorarios.Services.Interfaces;

namespace HackathonHealthMed.GestaoHorarios.Services
{
    public class HorarioConsultaService : IHorarioConsultaService
    {
        private readonly GestaoHorarioDbContext _context;

        public HorarioConsultaService(GestaoHorarioDbContext gestaoHorarioContext)
        {
            _context = gestaoHorarioContext;
        }
        public void AdicionarHorarioConsulta(HorarioConsulta horarioConsulta)
        {
            horarioConsulta.Id = Guid.NewGuid();
            _context.HorarioConsulta.Add(horarioConsulta);
            _context.SaveChanges();
        }

        public void AtualizarHorarioConsulta(HorarioConsulta horarioConsulta)
        {
            _context.HorarioConsulta.Update(horarioConsulta);
            _context.SaveChanges();
        }

        public List<HorarioConsulta> ConsultarHorariosPorData(DateTime horario)
        {
            return _context.HorarioConsulta.Where(x => x.HorarioInicial.Date == horario.Date).ToList();
        }

        public void ExcluirContato(Guid IdHorarioConsulta)
        {
            _context.HorarioConsulta.Remove(_context.HorarioConsulta.Find(IdHorarioConsulta));
        }

        public List<HorarioConsulta> ListarHorariosConsulta()
        {
            return _context.HorarioConsulta.ToList();
        }

        public bool ValidaHorarioPorMedico(DateTime horario, string crmMedico)
        {
            return _context.HorarioConsulta.Any(x => x.HorarioInicial.Date == horario.Date && x.MedicoCrm == crmMedico);
        }
    }
}
