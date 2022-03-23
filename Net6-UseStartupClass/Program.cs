using Autofac;
using Autofac.Extensions.DependencyInjection;
using Net6_UseStartupClass;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();
