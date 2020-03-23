using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using works.ei8.Cortex.Diary.Common;

namespace works.ei8.Cortex.Diary.Nucleus.Application
{
    internal static class Extensions
    {
        internal static Graph.Common.RelativeType ToExternalType(this RelativeType value)
        {
            return (Graph.Common.RelativeType)Enum.Parse(typeof(Graph.Common.RelativeType), ((int)value).ToString());
        }

        internal static Graph.Common.NeuronQuery ToExternalType(this NeuronQuery value)
        {
            var result = new Graph.Common.NeuronQuery();
            result.Id = value.Id?.ToArray();
            result.IdNot = value.IdNot?.ToArray();
            result.Postsynaptic = value.Postsynaptic?.ToArray();
            result.PostsynapticNot = value.PostsynapticNot?.ToArray();
            result.Presynaptic = value.Presynaptic?.ToArray();
            result.PresynapticNot = value.PresynapticNot?.ToArray();
            result.TagContains = value.TagContains?.ToArray();
            result.TagContainsNot = value.TagContainsNot?.ToArray();
            return result;
        }

        internal static IEnumerable<Neuron> ToInternalType(this IEnumerable<Graph.Common.Neuron> value)
        {
            return value.Select(cn => new Neuron()
            {
                Id = cn.Id,
                Tag = cn.Tag,
                Terminal = cn.Terminal != null ? new Terminal()
                {
                    AuthorId = cn.Terminal.AuthorId,
                    AuthorTag = cn.Terminal.AuthorTag,
                    Effect = cn.Terminal.Effect,
                    Id = cn.Terminal.Id,
                    PostsynapticNeuronId = cn.Terminal.PostsynapticNeuronId,
                    PresynapticNeuronId = cn.Terminal.PresynapticNeuronId,
                    Strength = cn.Terminal.Strength,
                    Timestamp = cn.Terminal.Timestamp,
                    Version = cn.Terminal.Version
                } : null,
                Version = cn.Version,
                AuthorId = cn.AuthorId,
                AuthorTag = cn.AuthorTag,
                LayerId = cn.LayerId,
                LayerTag = cn.LayerTag,
                Timestamp = cn.Timestamp,
                Errors = cn.Errors
            });
        }

        internal static NotificationLog ToInternalType(this EventSourcing.Common.NotificationLog value)
        {
            return new NotificationLog()
            {
                FirstNotificationLogId = value.FirstNotificationLogId,
                HasFirstNotificationLog = value.HasFirstNotificationLog,
                HasNextNotificationLog = value.HasNextNotificationLog,
                HasPreviousNotificationLog = value.HasPreviousNotificationLog,
                IsArchived = value.IsArchived,
                NextNotificationLogId = value.NextNotificationLogId,
                NotificationList = new ReadOnlyCollection<Common.Notification>(value.NotificationList.Select(n => n.ToInternalType()).ToList()),
                NotificationLogId = value.NotificationLogId,
                PreviousNotificationLogId = value.PreviousNotificationLogId,
                TotalCount = value.TotalCount,
                TotalNotification = value.TotalNotification
            };
        }

        internal static Common.Notification ToInternalType(this EventSourcing.Common.Notification value)
        {
            return new Common.Notification()
            {
                AuthorId = value.AuthorId,
                Data = value.Data,
                Id = value.Id,
                SequenceId = value.SequenceId,
                Timestamp = value.Timestamp,
                TypeName = value.TypeName,
                Version = value.Version
            };
        }
    }
}
