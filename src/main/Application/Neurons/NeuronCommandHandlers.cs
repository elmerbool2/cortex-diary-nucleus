﻿using CQRSlite.Commands;
using neurUL.Common.Domain.Model;
using neurUL.Common.Http;
using neurUL.Cortex.Client.In;
using System;
using System.Threading;
using System.Threading.Tasks;
using ei8.Cortex.Diary.Nucleus.Application.Neurons.Commands;
using ei8.Data.Aggregate.Client.In;
using ei8.Data.Tag.Client.In;

namespace ei8.Cortex.Diary.Nucleus.Application.Neurons
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
                this.settingsService.CortexInBaseUrl + "/", 
                message.Id.ToString(), 
                message.AuthorId.ToString(), 
                token
                );
            // increment expected
            expectedVersion++;
            // assign tag value
            await this.tagClient.ChangeTag(
                this.settingsService.TagInBaseUrl + "/",
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
                    this.settingsService.AggregateInBaseUrl + "/",
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
                this.settingsService.TagInBaseUrl + "/",
                message.Id.ToString(),
                message.NewTag,
                message.ExpectedVersion,
                message.AuthorId.ToString(),
                token
                );
        }

        public async Task Handle(DeactivateNeuron message, CancellationToken token = default(CancellationToken))
        {
            AssertionConcern.AssertArgumentNotNull(message, nameof(message));

            await this.neuronClient.DeactivateNeuron(
                this.settingsService.CortexInBaseUrl + "/",
                message.Id.ToString(),
                message.ExpectedVersion,
                message.AuthorId.ToString(),
                token
                );            
        }
    }
}