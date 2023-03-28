using TaskTracker.BackgroundTasks;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<CheckerOldTasks>();
    })
    .Build();

await host.RunAsync();
