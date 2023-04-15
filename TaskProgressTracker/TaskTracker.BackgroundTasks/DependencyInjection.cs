using TaskTracker.BackgroundTasks;
using TaskTracker.Infrastructure;
using TaskTracker.Infrastructure.Persistence.Context;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<SQLServerContext>();
        services.AddHostedService<CheckerNewTasks>();
        services.AddInfrastructurePersistence(hostContext.Configuration);
        services.AddInfrastructureConnections();
    })
    .Build();

await host.RunAsync();
