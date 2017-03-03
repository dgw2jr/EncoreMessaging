using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using EncoreMessages;
using NServiceBus;

namespace MVCMessagingTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var endpoint = ConfigureEndpoint();

            var container = BuildContainer(endpoint);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static IContainer BuildContainer(IEndpointInstance endpointInstance)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(endpointInstance).AsImplementedInterfaces();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            return builder.Build();
        }

        private static IEndpointInstance ConfigureEndpoint()
        {
            var endpointConfiguration = new EndpointConfiguration($"EncoreMessageSenderMVC");
            endpointConfiguration.MakeInstanceUniquelyAddressable(Guid.NewGuid().ToString());

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            var routing = transport.Routing();

            routing.RouteToEndpoint(typeof(SendChatMessageCommand).Assembly, "EncoreMessageReceiver");
            routing.RegisterPublisher(typeof(ChatMessageSentEvent).Assembly, "EncoreMessageReceiver");

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            return Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
        }
    }
}
