using Autofac;
using Net6_UseStartupClass.Code;
using Serilog;

namespace Net6_UseStartupClass
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Changes to the JSON configuration file after the app has started are not read.
            // To read changes after the app has started, use IOptionsSnapshot.
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0#options-interfaces
            services.Configure<AppSettings>(Configuration.GetSection(AppSettings.Key));

            // Can use this Approach for Option Validations
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0#options-validation
            services.AddOptions<TokenSettings>().Bind(Configuration.GetSection(TokenSettings.Key));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        //  If using a custom DI container
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Sample
            // builder.RegisterType<MultiTenantResolver>().As<ITenantResolver>().InstancePerLifetimeScope();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging(); // <-- Add this line

            //app.UseSerilogRequestLogging(options =>
            //{
            //    options.MessageTemplate = "Serilog Sample: HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
            //});

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
