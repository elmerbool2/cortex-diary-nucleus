using CQRSlite.Commands;
using CQRSlite.Routing;
using Nancy;
using Nancy.TinyIoc;
using org.neurul.Common.Http;
using org.neurul.Cortex.Client.In;
using System;
using works.ei8.Cortex.Diary.Nucleus.Application;
using works.ei8.Cortex.Diary.Nucleus.Application.Neurons;
using works.ei8.Cortex.Diary.Nucleus.Port.Adapter.IO.Process.Services;
using works.ei8.Data.Tag.Client.In;

namespace works.ei8.Cortex.Diary.Nucleus.Port.Adapter.In.Api
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        public CustomBootstrapper()
        {
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            // create a singleton instance which will be reused for all calls in current request
            var ipb = new Router();
            container.Register<ICommandSender, Router>(ipb);
            container.Register<IHandlerRegistrar, Router>(ipb);
            container.Register<IRequestProvider, RequestProvider>();
            container.Register<INeuronClient, HttpNeuronClient>();
            container.Register<ITagClient, HttpTagClient>();
            container.Register<ISettingsService, SettingsService>();
            container.Register<NeuronCommandHandlers>();

            // TODO: necessary?
            var ticl = new TinyIoCServiceLocator(container);
            container.Register<IServiceProvider, TinyIoCServiceLocator>(ticl);
            var registrar = new RouteRegistrar(ticl);
            registrar.Register(typeof(NeuronCommandHandlers));

            ((TinyIoCServiceLocator)container.Resolve<IServiceProvider>()).SetRequestContainer(container);
        }
    }
}
