using HackathonHealthMed.GestaoHorarios.Models;

namespace HackathonHealthMed.GestaoHorarios.Services.Interfaces
{
    public interface IValorConsultaMedicoService
    {
        public ValorConsultaMedico ObterValorConsulta(string crmMedico);
        public void AtualizarValorConsulta(ValorConsultaMedico valorConsultaMedicoAtualizado);
        public void AdicionaValorConsulta(ValorConsultaMedico valorConsultaMedico);
        public bool ValidaExistenciaConsultaPorMedico(string crmMedico);
    }
}
