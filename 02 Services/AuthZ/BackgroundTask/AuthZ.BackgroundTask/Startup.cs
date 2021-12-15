using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthZ.BackgroundTask.Infrastructure.AutofacModules;
using AuthZ.BackgroundTask.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using Sunedu.Siu.Infrastucture.Extensions;
using AuthZ.BackgroundTask.Infrastructure.Filters;
using AuthZ.BackgroundTask.Config;

namespace AuthZ.BackgroundTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //add health check for this service
            services.AddCustomMvc(Configuration)
                    .AddCustomHealthChecks(Configuration)
                    //.AddCustomDbContext(Configuration)
                    //.AddCustomConfigureEventBus(Configuration)
                    //.AddCustomRegisterEventBus(Configuration)
                    .AddCustomIntegrations(Configuration);

            //Options API
            services.AddOptions<CustomBgSetting>().Bind(Configuration.GetSection(CustomBgSetting.Name));

            services.AddMongoDB(Configuration);

            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));
            return new AutofacServiceProvider(container.Build());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }
            app.UseRouting();
            //app.UseCustomHealthChecks();
            //app.UseHttpsRedirection();
            //app.UseMvc();º
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });

            });
        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));

            })
             .AddControllersAsServices()
             .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            return services;
        }
        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<DatosRolesHostedService>(); 
            services.AddHostedService<DatosAplicacionesHostedService>();

            return services;
        }

        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var mongoDBConnectionString = configuration["MongoDBSettings:ConnectionString"];
                var mongoDBDatabase = configuration["MongoDBSettings:Database"];
                var client = new MongoClient(mongoDBConnectionString);

                return client.GetDatabase(mongoDBDatabase);
            });

            return services;
        }
    }

}
