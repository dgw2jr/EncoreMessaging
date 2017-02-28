using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using EncoreMessages;
using NServiceBus;

namespace EncoreMessageSender
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildContainer();
            var endpoint = ConfigureEndpoint(container);

            using (var scope = container.BeginLifetimeScope())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(scope.Resolve<Form1>(parameters: TypedParameter.From(endpoint)));
            }
        }

        private static IEndpointInstance ConfigureEndpoint(IContainer container)
        {
            var endpointConfiguration = new EndpointConfiguration($"EncoreMessageSender-{Environment.UserName}_{Environment.MachineName}");

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            var routing = transport.Routing();

            routing.RouteToEndpoint(typeof(SendChatMessageCommand).Assembly, "EncoreMessageReceiver");
            routing.RegisterPublisher(typeof(ChatMessageSentEvent).Assembly, "EncoreMessageReceiver");

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            endpointConfiguration.UseContainer<AutofacBuilder>(customizations =>
            {
                customizations.ExistingLifetimeScope(container);
            });

            return Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Form1>().AsSelf();
            builder.RegisterType<HistoryContainer>().AsSelf().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsClosedTypesOf(typeof(IHTMLFormatter<>));

            return builder.Build();
        }
    }
}
