
using System.ServiceProcess;
using WIndowsServices.Microsoft;

var service = new CustomService();

if (Environment.UserInteractive)
{
    service.Start(null);

    Console.ReadLine();

    service.Stop();
}
else
    ServiceBase.Run(service);