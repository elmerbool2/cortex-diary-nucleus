using CQRSlite.Events;
using CQRSlite.Routing;
using Nancy;
using Nancy.TinyIoc;
using org.neurul.Common.Http;
using works.ei8.Cortex.Diary.Nucleus.Application;
using works.ei8.Cortex.Diary.Nucleus.Application.Neurons;
using works.ei8.Cortex.Diary.Nucleus.Application.Notification;
using works.ei8.Cortex.Diary.Nucleus.Port.Adapter.IO.Process.Services;
using works.ei8.Cortex.Graph.Client;
using works.ei8.EventSourcing.Client;
using works.ei8.EventSourcing.Client.Out;

namespace works.ei8.Cortex.Diary.Nucleus.Port.Adapter.Out.Api
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        public CustomBootstrapper()
        {
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            container.Register<IRequestProvider, RequestProvider>();
            container.Register<ISettingsService, SettingsService>();
            container.Register<INeuronGraphQueryClient, HttpNeuronGraphQueryClient>();
            container.Register<INeuronQueryService, NeuronQueryService>();
            container.Register<IEventSerializer, EventSerializer>();
            container.Register<INotificationClient, HttpNotificationClient>();
            container.Register<INotificationApplicationService, NotificationApplicationService>();
        }
    }
}
