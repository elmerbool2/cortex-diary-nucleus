﻿using CQRSlite.Commands;
using neurUL.Common.Domain.Model;
using System;

namespace ei8.Cortex.Diary.Nucleus.Application.Neurons.Commands
{
    public class ChangeNeuronTag : ICommand
    {
        public ChangeNeuronTag(Guid id, string newTag, Guid authorId, int expectedVersion)
        {
            AssertionConcern.AssertArgumentValid(
                g => g != Guid.Empty,
                id,
                Messages.Exception.InvalidId,
                nameof(id)
                );
            AssertionConcern.AssertArgumentNotNull(newTag, nameof(newTag));
            AssertionConcern.AssertArgumentValid(
                g => g != Guid.Empty,
                authorId,
                Messages.Exception.InvalidId,
                nameof(authorId)
                );
            AssertionConcern.AssertArgumentValid(
                i => i >= 1,
                expectedVersion,
                Messages.Exception.InvalidExpectedVersion,
                nameof(expectedVersion)
                );

            this.Id = id;            
            this.NewTag = newTag;
            this.AuthorId = authorId;
            this.ExpectedVersion = expectedVersion;
        }

        public Guid Id { get; private set; }

        public string NewTag { get; private set; }

        public Guid AuthorId { get; private set; }

        public int ExpectedVersion { get; private set; }
    }
}
