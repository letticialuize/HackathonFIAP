using HackathonHealthMed.Agendamentos.Services.Interfaces;
using HackathonHealthMed.Contracts;
using MassTransit;

namespace HackathonHealthMed.Agendamentos.Eventos
{
    public class AtualizaStatusConsultaConsumer : IConsumer<AgendamentoContract>
    {
        private readonly IAgendamentoService _agendamentoService;

        public AtualizaStatusConsultaConsumer(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        public Task Consume(ConsumeContext<AgendamentoContract> context)
        {
            _agendamentoService.AtualizaStatusAgendamento(context.Message.Id, context.Message.Status);

            return Task.CompletedTask;
        }
    }
}
