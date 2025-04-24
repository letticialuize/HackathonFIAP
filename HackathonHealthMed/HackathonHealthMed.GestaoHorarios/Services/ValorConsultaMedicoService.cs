using HackathonHealthMed.GestaoHorarios.Data;
using HackathonHealthMed.GestaoHorarios.Models;
using HackathonHealthMed.GestaoHorarios.Services.Interfaces;

namespace HackathonHealthMed.GestaoHorarios.Services
{
    public class ValorConsultaMedicoService : IValorConsultaMedicoService
    {
        private readonly GestaoHorarioDbContext _context;
        public ValorConsultaMedicoService(GestaoHorarioDbContext context)
        {
            _context = context;
        }

        public void AdicionaValorConsulta(ValorConsultaMedico valorConsultaMedico)
        {
            valorConsultaMedico.Id = Guid.NewGuid();
            _context.ValorConsultaMedico.Add(valorConsultaMedico);
            _context.SaveChanges();
        }

        public void AtualizarValorConsulta(ValorConsultaMedico valorConsultaMedicoAtualizado)
        {
            _context.ValorConsultaMedico.Update(valorConsultaMedicoAtualizado);
            _context.SaveChanges();
        }

        public ValorConsultaMedico ObterValorConsulta(string crmMedico)
        {
           return _context.ValorConsultaMedico.FirstOrDefault(x => x.MedicoCrm == crmMedico);
        }

        public bool ValidaExistenciaConsultaPorMedico(string crmMedico)
        {
            return _context.ValorConsultaMedico.Any(x => x.MedicoCrm == crmMedico);
        }
    }
}
