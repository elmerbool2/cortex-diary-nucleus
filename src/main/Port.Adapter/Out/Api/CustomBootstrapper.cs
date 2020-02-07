using CQRSlite.Events;
using CQRSlite.Routing;
using Nancy;
using Nancy.TinyIoc;
using works.ei8.Cortex.Diary.Nucleus.Application.Notification;

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

            var ipb = new Router();
            // TODO: container.Register<IEventPublisher, Router>(ipb);
            //container.Register<IEventSerializer, EventSerializer>();
            //container.Register<IEventSourceFactory, EventSourceFactory>();
            container.Register<INotificationApplicationService, NotificationApplicationService>();
        }
    }
}
