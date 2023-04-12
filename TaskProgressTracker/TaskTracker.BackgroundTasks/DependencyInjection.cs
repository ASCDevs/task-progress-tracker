using TaskTracker.BackgroundTasks;
using TaskTracker.Infrastructure;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<CheckerNewTasks>();
        services.AddInfrastructurePersistence(hostContext.Configuration);
        services.AddInfrastructureConnections();
    })
    .Build();

await host.RunAsync();
