using HackathonHealthMed.GestaoHorarios.Data;
using HackathonHealthMed.GestaoHorarios.DTOs;
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

        public List<HorarioConsulta> ConsultarHorariosPorDataECrm(DateTime horario, string crm)
        {
            return _context.HorarioConsulta.Where(x => x.HorarioInicial.Date == horario.Date && x.MedicoCrm == crm).ToList();
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
            return _context.HorarioConsulta.Any(x => horario >= x.HorarioInicial && horario < x.HorarioFinal && x.MedicoCrm == crmMedico);
        }

        public List<AgendaMedicoDTO> ConsultarHorariosDisponiveisPorCrm(string crm)
        {
            decimal valorConsulta = _context.ValorConsultaMedico.FirstOrDefault(x => x.MedicoCrm == crm)?.Valor ?? 0;

            return _context.HorarioConsulta.Where(x => x.MedicoCrm == crm && x.EstaDisponivel)
                                           .Select(x => new AgendaMedicoDTO()
                                           {
                                               Id = x.Id,
                                               MedicoCrm = x.MedicoCrm,
                                               ValorConsulta = valorConsulta,
                                               HorarioInicial = x.HorarioInicial,
                                               HorarioFinal = x.HorarioFinal,
                                               EstaDisponivel = x.EstaDisponivel
                                           })
                                           .ToList();
        }

        public void OcupaHorarioDisponivel(Guid id)
        {
            var horarioConsulta = _context.HorarioConsulta.Find(id);
            horarioConsulta.EstaDisponivel = false;
            _context.HorarioConsulta.Update(horarioConsulta);
            _context.SaveChanges();
        }
    }
}
