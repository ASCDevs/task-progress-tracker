using TaskTracker.BackgroundTasks;
using TaskTracker.Infrastructure.ConnectionsServices;
using TaskTracker.Infrastructure.Interfaces;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<CheckerOldTasks>();
        services.AddSingleton<IConnectionTaskServer, ConnectionTaskServer>();
    })
    .Build();

await host.RunAsync();
