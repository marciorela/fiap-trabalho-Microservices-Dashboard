using Geekburger.Dashboard.Data;
using Geekburger.Dashboard.Database;
using Geekburger.Dashboard.Services;
using Geekburger.Dashboard.WS;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<DashboardDbContext>();
        services.AddSingleton<OrderRepository>();
        services.AddSingleton<RestrictionRepository>();
        services.AddSingleton<RestrictionService>();
        services.AddSingleton<SalesService>();
        //services.AddHostedService<WorkerMessageOrderChanged>();
        services.AddHostedService<WorkerMessageUserWithLessOffer>();
    })
    .ConfigureAppConfiguration((services, configBuilder) =>
    {
        configBuilder
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();
    })
    .Build();

await host.RunAsync();
