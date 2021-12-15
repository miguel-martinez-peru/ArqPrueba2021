using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AuthZ.Api.Extensions;
using AuthZ.Api.Infrastructure.AutofacModules;
using AuthZ.Api.Infrastructure.Filters;
using AuthZ.Api.Infrastructure.Middlewares;
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
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Serilog;
using Sunedu.Siu.Infrastucture.Extensions;
using Sunedu.Transversal.Security.Auth.Filters;

namespace AuthZ.Api
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
            services
                .AddDirectoryBrowser()
                .AddMongoDB(Configuration);

            services.AddCustomMvc(Configuration)
                .AddCustomHealthChecks(Configuration)
                .AddCustomSwagger(Configuration, Program.AppName)
                //Punku
                .AddCustomAuthentication(Configuration)
                .AddIntegrationTracerServices(Configuration, Program.AppName);

            //Punku
            services.AddScoped<AuthorizeCheckActionFilter>();

            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration["SuffixNameBd"], Configuration["ConnectionString"]));
            return new AutofacServiceProvider(container.Build());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            app.UseCors("CorsPolicy");

            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "AuthZ.Api V1");
                  c.OAuthClientId("authzswaggerui");
                  c.OAuthAppName("AuthZ Swagger UI");
              });

            app.UseRouting();
            ConfigureAuth(app);

            //app.UseStaticFiles();
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

        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            if (Configuration.GetValue<bool>("UseLoadTest"))
            {
                app.UseMiddleware<ByPassAuthMiddleware>();
            }

            //PUNKU
            app.UseAuthentication();
            app.UseAuthorization();
        }

    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            var CorsOriginAllowed = configuration.GetSection("AllowedOrigins").Get<List<string>>();
            ///TODO: Si no se registra un origen en [AllowedOrigins] se asigna [*] para responder a cualquier origen por defecto
            var origins = CorsOriginAllowed != null ? CorsOriginAllowed.ToArray() : new string[] { "*" };

            Log.Information("Configurando Origenes para ({CORS})...", origins);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                     .SetIsOriginAllowed((host) => true)
                    //.WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.AllowCredentials()
                    );
            });
            Log.Information("Fin de Configuración ({CORS})...");


            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddControllersAsServices();

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
