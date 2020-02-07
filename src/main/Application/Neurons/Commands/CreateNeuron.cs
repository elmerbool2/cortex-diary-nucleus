using CQRSlite.Commands;
using Newtonsoft.Json;
using org.neurul.Common.Domain.Model;
using System;

namespace works.ei8.Cortex.Diary.Nucleus.Application.Neurons.Commands
{
    public class CreateNeuron : ICommand
    {
        public CreateNeuron(string avatarId, Guid id, string tag, Guid regionId, Guid authorId)
        {
            AssertionConcern.AssertArgumentNotNull(avatarId, nameof(avatarId));
            AssertionConcern.AssertArgumentValid(
                g => g != Guid.Empty,
                id,
                Messages.Exception.InvalidId,
                nameof(id)
                );
            AssertionConcern.AssertArgumentNotNull(tag, nameof(tag));
            AssertionConcern.AssertArgumentValid(
                g => g != Guid.Empty,
                authorId,
                Messages.Exception.InvalidId,
                nameof(authorId)
                );

            this.AvatarId = avatarId;
            this.Id = id;            
            this.Tag = tag;
            this.RegionId = regionId;
            this.AuthorId = authorId;
        }

        public string AvatarId { get; private set; }

        public Guid Id { get; private set; }
        
        public string Tag { get; private set; }

        public Guid RegionId { get; private set; }

        public Guid AuthorId { get; private set; }

        public int ExpectedVersion { get; private set; }
    }
}
