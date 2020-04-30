using System;
using System.Collections.Generic;
using System.Text;
using ei8.EventSourcing.Client;
using ei8.EventSourcing.Client.Out;

namespace ei8.Cortex.Diary.Nucleus.Port.Adapter.IO.Process.Services
{
    public class EventSourceFactory : IEventSourceFactory
    {
        public IEventSource Create(string inStoreUrl, string outStoreUrl, Guid authorId)
        {
            var nc = new HttpNotificationClient();
            return new EventSource(inStoreUrl, outStoreUrl, null, null, null, nc);
        }
    }
}
