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
                var service1 = new Service1();
                service1.TestStartupAndStop(args);
            }
            else
            {
                var ServicesToRun = new ServiceBase[]
                {
                    new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }

        }
    }
}
