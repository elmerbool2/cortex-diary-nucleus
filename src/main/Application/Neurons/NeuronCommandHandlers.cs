using CQRSlite.Commands;
using org.neurul.Common.Domain.Model;
using org.neurul.Common.Http;
using org.neurul.Cortex.Client.In;
using System;
using System.Threading;
using System.Threading.Tasks;
using works.ei8.Cortex.Diary.Nucleus.Application.Neurons.Commands;
using works.ei8.Data.Aggregate.Client.In;
using works.ei8.Data.Tag.Client.In;

namespace works.ei8.Cortex.Diary.Nucleus.Application.Neurons
{
    public class NeuronCommandHandlers : 
        ICancellableCommandHandler<CreateNeuron>,
        ICancellableCommandHandler<ChangeNeuronTag>,
        ICancellableCommandHandler<DeactivateNeuron>
    {
        private readonly INeuronClient neuronClient;
        private readonly ITagClient tagClient;
        private readonly IAggregateClient aggregateClient;
        private readonly ISettingsService settingsService;

        public NeuronCommandHandlers(INeuronClient neuronClient, ITagClient tagClient, IAggregateClient aggregateClient, ISettingsService settingsService)
        {
            AssertionConcern.AssertArgumentNotNull(neuronClient, nameof(neuronClient));
            AssertionConcern.AssertArgumentNotNull(tagClient, nameof(tagClient));
            AssertionConcern.AssertArgumentNotNull(aggregateClient, nameof(aggregateClient));
            AssertionConcern.AssertArgumentNotNull(settingsService, nameof(settingsService));

            this.neuronClient = neuronClient;
            this.tagClient = tagClient;
            this.aggregateClient = aggregateClient;
            this.settingsService = settingsService;
        }

        public async Task Handle(CreateNeuron message, CancellationToken token = default(CancellationToken))
        {
            AssertionConcern.AssertArgumentNotNull(message, nameof(message));

            //TODO: transfer all of this to Domain.Model, especially parse of Guid for region/aggregate
            int expectedVersion = 0;
            await this.neuronClient.CreateNeuron(
                Helper.UrlCombine(this.settingsService.CortexInBaseUrl, message.AvatarId) + "/", 
                message.Id.ToString(), 
                message.AuthorId.ToString(), 
                token
                );
            // increment expected
            expectedVersion++;
            // assign tag value
            await this.tagClient.ChangeTag(
                Helper.UrlCombine(this.settingsService.TagInBaseUrl, message.AvatarId) + "/",
                message.Id.ToString(),
                message.Tag,
                expectedVersion,
                message.AuthorId.ToString(),
                token
                );
            if (message.RegionId != Guid.Empty)
            {
                // increment expected
                expectedVersion++;
                // assign region value to id
                await this.aggregateClient.ChangeAggregate(
                    Helper.UrlCombine(this.settingsService.AggregateInBaseUrl, message.AvatarId) + "/",
                    message.Id.ToString(),
                    message.RegionId.ToString(),
                    expectedVersion,
                    message.AuthorId.ToString(),
                    token
                    );
            }
        }

        public async Task Handle(ChangeNeuronTag message, CancellationToken token = default(CancellationToken))
        {
            AssertionConcern.AssertArgumentNotNull(message, nameof(message));

            await this.tagClient.ChangeTag(
                Helper.UrlCombine(this.settingsService.TagInBaseUrl, message.AvatarId) + "/",
                message.Id.ToString(),
                message.NewTag,
                message.ExpectedVersion,
                message.AuthorId.ToString(),
                token
                );
        }

        public async Task Handle(DeactivateNeuron message, CancellationToken token = default(CancellationToken))
        {
            //AssertionConcern.AssertArgumentNotNull(message, nameof(message));

            //var eventSource = this.eventSourceFactory.CreateEventSource(message.AvatarId);
            //await this.userRepository.Initialize(message.AvatarId);
            //await this.layerPermitRepository.Initialize(message.AvatarId);

            //// TODO: Add TDD test
            //var author = await NeuronCommandHandlers.GetValidatedAuthorUser(message.SubjectId, eventSource.Session, this.userRepository, this.layerPermitRepository, token);
            //Neuron neuron = await eventSource.Session.Get<Neuron>(message.Id, nameof(neuron), message.ExpectedVersion, token);
            //Neuron layer = await eventSource.Session.GetOrDefaultIfGuidEmpty(
            //        neuron.LayerId,
            //        nameof(layer),
            //        Neuron.RootLayerNeuron,
            //        cancellationToken: token
            //        );

            //neuron.Deactivate(layer, author);
            //await eventSource.Session.Commit(token);

            await Task.CompletedTask;
        }
    }
}