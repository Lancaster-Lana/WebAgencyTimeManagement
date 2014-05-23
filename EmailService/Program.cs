using System.ServiceProcess;

namespace EmailService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var ServicesToRun = new ServiceBase[] 
			{ 
				new EmailService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
