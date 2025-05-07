using HackathonHealthMed.Contracts;
using HackathonHealthMed.GestaoHorarios.Services.Interfaces;
using MassTransit;
using System.Text.Json;

namespace HackathonHealthMed.GestaoHorarios.Eventos
{
    public class OcupaHorarioConsumer : IConsumer<AgendamentoContract>
    {
        private readonly IHorarioConsultaService _horarioConsultaService;

        public OcupaHorarioConsumer(IHorarioConsultaService horarioConsultaService)
        {
            _horarioConsultaService=horarioConsultaService;
        }

        public Task Consume(ConsumeContext<AgendamentoContract> context)
        {
            _horarioConsultaService.OcupaHorarioDisponivel(context.Message.HorarioConsultaId);

            return Task.CompletedTask;
        }
    }
}
