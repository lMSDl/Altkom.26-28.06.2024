

using Topshelf;
using WIndowsServices.TopShelf;

HostFactory.Run(x =>
{
    x.Service<CustomService>();
    x.SetServiceName("CustomTopShelfService");
    x.SetDescription("viaTopShelf");

    x.EnableServiceRecovery(x =>
    {
        x
        .RestartService(TimeSpan.FromSeconds(1))
        .RestartService(TimeSpan.FromSeconds(3))
        .RestartService(TimeSpan.FromSeconds(5))
        .SetResetPeriod(5);
    });

    x.DependsOn("microsoft");
    x.RunAsLocalSystem();

    x.StartAutomaticallyDelayed();
});