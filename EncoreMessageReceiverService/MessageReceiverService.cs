using System;
using System.Configuration;
using System.ServiceProcess;
using Autofac;
using DataAccess;
using NServiceBus;

namespace EncoreMessageReceiverService
{
    public partial class MessageReceiverService : ServiceBase
    {
        public MessageReceiverService()
        {
            InitializeComponent();

            AutoLog = true;
        }

        public IEndpointInstance EndpointInstance { get; set; }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Starting Encore messaging receiver endpoint...");

            EndpointInstance = StartEndpoint();

            EventLog.WriteEntry("Started Encore messaging receiver endpoint.");
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Stopping Encore messaging receiver endpoint...");

            EndpointInstance.Stop();

            EventLog.WriteEntry("Stopped Encore messaging receiver endpoint.");
        }

        internal void TestStartupAndStop(string[] args)
        {
            OnStart(args);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);

            OnStop();
        }

        private static IEndpointInstance StartEndpoint()
        {
            var endpointConfiguration = new EndpointConfiguration(ConfigurationManager.AppSettings["endpointName"]);

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var builder = new ContainerBuilder();
            builder.RegisterType<DomainModel>().AsSelf().AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(GenericRepository<>)).AsImplementedInterfaces();
            var container = builder.Build();

            endpointConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));

            var endpointInstance = Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            return endpointInstance.GetAwaiter().GetResult();
        }
    }
}
