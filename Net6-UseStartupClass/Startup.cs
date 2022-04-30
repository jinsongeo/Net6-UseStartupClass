using Autofac;
using Net6_UseStartupClass.Code;
using Net6_UseStartupClass.Code.JsonSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                        {
                            // Resolves UserName => user_name
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        };
                        // options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeHtml;
                        options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;

                        // Note: Need to disable DateParseHandling because if in api we receive string that looks like date,
                        // json.net by default automatically converts it to date and field becomes modified, iso date is converted to another format
                        options.SerializerSettings.DateParseHandling = DateParseHandling.None;
                        options.SerializerSettings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;

                        options.SerializerSettings.Converters.Add(new StringEnumConverter());

                        //Configure the API Json Serializer
                        ApiJsonSerializer.Configure(options.SerializerSettings);
                    });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        //  If using a custom DI container
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Sample
            // builder.RegisterType<MultiTenantResolver>().As<ITenantResolver>().InstancePerLifetimeScope();

            builder.RegisterType<ApiJsonSerializer>().As<IApiJsonSerializer>().SingleInstance();
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
