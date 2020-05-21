using CQRSlite.Commands;
using ei8.Cortex.Diary.Nucleus.Application.Neurons.Commands;
using neurUL.Common.Domain.Model;
using neurUL.Cortex.Client.In;
using System.Threading;
using System.Threading.Tasks;

namespace ei8.Cortex.Diary.Nucleus.Application.Neurons
{
    public class TerminalCommandHandlers :
        ICancellableCommandHandler<CreateTerminal>,
        ICancellableCommandHandler<DeactivateTerminal>
    {
        private readonly ITerminalClient terminalClient;
        private readonly ISettingsService settingsService;

        public TerminalCommandHandlers(ITerminalClient terminalClient, ISettingsService settingsService)
        {
            AssertionConcern.AssertArgumentNotNull(terminalClient, nameof(terminalClient));
            AssertionConcern.AssertArgumentNotNull(settingsService, nameof(settingsService));

            this.terminalClient = terminalClient;
            this.settingsService = settingsService;
        }

        public async Task Handle(CreateTerminal message, CancellationToken token = default(CancellationToken))
        {
            AssertionConcern.AssertArgumentNotNull(message, nameof(message));

            await this.terminalClient.CreateTerminal(
                this.settingsService.CortexInBaseUrl + "/",
                message.Id.ToString(),
                message.PresynapticNeuronId.ToString(),
                message.PostsynapticNeuronId.ToString(),
                message.Effect,
                message.Strength,
                message.AuthorId.ToString(),
                token
                );
        }

        public async Task Handle(DeactivateTerminal message, CancellationToken token = default(CancellationToken))
        {
            AssertionConcern.AssertArgumentNotNull(message, nameof(message));

            await this.terminalClient.DeactivateTerminal(
                this.settingsService.CortexInBaseUrl + "/",
                message.Id.ToString(),
                message.ExpectedVersion,
                message.AuthorId.ToString(),
                token
                );
        }
    }
}
