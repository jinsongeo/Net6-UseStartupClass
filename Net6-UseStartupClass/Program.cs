using Autofac;
using Autofac.Extensions.DependencyInjection;
using Net6_UseStartupClass;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day)
            .WriteTo.File("logs/errorlog.txt", restrictedToMinimumLevel: LogEventLevel.Warning)
            .WriteTo.Seq(serverUrl: "http://localhost:5341")
            .CreateBootstrapLogger();


try
{
    Log.Information("Starting Application....");

    var builder = WebApplication.CreateBuilder(args);

    // Full setup of serilog. We read log settings from appsettings.json
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    //Get Instance of Startup Class
    var startup = new Startup(builder.Configuration);

    //Configure Services    
    startup.ConfigureServices(builder.Services);

    //If using a custom DI container
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);

    //Get the Web Application
    var app = builder.Build();

    //Configure the Middleware
    startup.Configure(app, app.Environment);

    Log.Information("Run Application....");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host Terminated Unexpectedly!");
    return 1;
}
finally
{
    Log.Information("Exiting Application...");
    Log.CloseAndFlush();
}
return 0;
