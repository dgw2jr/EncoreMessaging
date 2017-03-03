using System;
using System.ServiceProcess;

namespace EncoreMessageReceiverService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                var service = new MessageReceiverService();
                service.TestStartupAndStop(args);
            }
            else
            {
                var ServicesToRun = new ServiceBase[]
                {
                    new MessageReceiverService(),
                };
                ServiceBase.Run(ServicesToRun);
            }

        }
    }
}
