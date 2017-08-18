using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Autofy.Public.Api.Interfaces;
using Microsoft.Extensions.Options;
using Autofy.Public.Api.DataProvider;
using Autofy.Public.Api.WebAdapter;

namespace Autofy.Public.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();


            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());
            });

            var settingsSection = Configuration.GetSection("Settings");
            services.Configure<Settings>(settingsSection);

            if(services.All(s => s.ServiceType != typeof(IDataProvider)))
            {
                services.AddSingleton<IDataProvider>(provider =>
                {
                    var options = provider.GetService<IOptions<Settings>>();
                    var settings = options.Value;

                    return new MongoDBDataProvider(settings.DataProviderConnectionString);
                });
            }
            if (services.All(s => s.ServiceType != typeof(IWebAdapter)))
            {
                services.AddSingleton<IWebAdapter>(provider =>
                {
                    return new GenericWebAdapter();
                });
            }

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("AllowAll");

            app.UseMvc();
        }
    }
}
