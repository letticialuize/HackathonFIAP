using HackathonHealthMed.Contracts;
using HackathonHealthMed.GestaoHorarios.Services.Interfaces;
using MassTransit;
using System.Text.Json;

namespace HackathonHealthMed.GestaoHorarios.Eventos
{
    public class DesocupaHorarioConsumer : IConsumer<AgendamentoContract>
    {
        private readonly IHorarioConsultaService _horarioConsultaService;

        public DesocupaHorarioConsumer(IHorarioConsultaService horarioConsultaService)
        {
            _horarioConsultaService=horarioConsultaService;
        }

        public Task Consume(ConsumeContext<AgendamentoContract> context)
        {
            _horarioConsultaService.DesocupaHorario(context.Message.HorarioConsultaId);

            return Task.CompletedTask;
        }
    }
}
