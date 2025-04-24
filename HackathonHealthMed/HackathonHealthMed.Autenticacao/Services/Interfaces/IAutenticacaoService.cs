using HackathonHealthMed.Autenticacao.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace HackathonHealthMed.Autenticacao.Services.Interfaces
{
    public interface IAutenticacaoService
    {
        ResponseMedicoDTO BuscarMedicoPorId(string userId);
        ResponseMedicoDTO BuscarMedicoPorCRM(string crm);
        ResponsePacienteDTO BuscarPacientePorId(string userId);
        ResponsePacienteDTO BuscarPacientePorCPF(string cpf);
        void AdicionarPaciente(RegistroPacienteDTO pacienteDTO, IdentityUser user);
        void AdicionarMedico(RegistroMedicoDTO medicoDTO, IdentityUser user);
        List<ResponseMedicoDTO> BuscarMedicosPorEspecialidade(string especialidade);

    }
}
