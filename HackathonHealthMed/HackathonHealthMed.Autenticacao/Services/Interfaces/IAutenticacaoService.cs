using HackathonHealthMed.Autenticacao.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace HackathonHealthMed.Autenticacao.Services.Interfaces
{
    public interface IAutenticacaoService
    {
        ResponseMedicoDTO BuscarMedicoPorId(string userId);
        ResponsePacienteDTO BuscarPacientePorId(string userId);
        void AdicionarPaciente(RegistroPacienteDTO pacienteDTO, IdentityUser user);
        void AdicionarMedico(RegistroMedicoDTO medicoDTO, IdentityUser user);

    }
}
