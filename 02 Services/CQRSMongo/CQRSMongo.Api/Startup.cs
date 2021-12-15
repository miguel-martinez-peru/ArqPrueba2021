using AcademicoOds.Api.Infrastructure.AutofacModules;
using AcademicoOds.Api.Infrastructure.Filters;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HealthChecks.UI.Client;
using Institucional.Api.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Sunedu.Siu.Infrastucture.Extensions;
using Sunedu.Transversal.Security.Auth.Config;
using Sunedu.Transversal.Security.Auth.Extensions;
using Sunedu.Transversal.Security.Auth.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AcademicoOds.Api
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
                    .AddCustomMvc(Configuration)
                    .AddCustomHealthChecks(Configuration)
                    .AddCustomSwagger(Configuration)
                    //Punku
                    .AddCustomAuthentication(Configuration)
                    .AddIntegrationTracerServices(Configuration, Program.AppName);

            //Punku
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<PunkuConfig>(Configuration.GetSection("Auth"));
            services.AddPunkuHttpClient();
            services.AddScoped<AuthorizeCheckActionFilter>();

            services.AddMemoryCache();

            services.AddControllers();

            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{pathBase}'");
                app.UsePathBase(pathBase);
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            ConfigureAuth(app);


            //app.UseAuthorization();

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Entidad Gateway V1");
                    //c.OAuthClientId("authzswaggerui");
                    //c.OAuthAppName("AuthZ Swagger UI");
                });

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
                    //.SetIsOriginAllowed((host) => IsOriginAllowed(host, origins))
                    .WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.AllowCredentials()
                    );
            });
            Log.Information("Fin de Configuración ({CORS})...");


            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .AddNewtonsoftJson()
            .AddControllersAsServices()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .ConfigureApiBehaviorOptions(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Por favor, consulte la propiedad de errores para más detalles."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = $"Servicio de entidades - Nodo [{System.Net.Dns.GetHostName()}]",
                    Version = "v1",
                    Description = "Documentación de los métodos que se exponen a las entidades externas",
                    //TermsOfService = "Terminos del servicio",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "soportesiu@sunedu.gob.pe",
                        Name = "Centro de atención",
                        Url = new Uri("https://www.gob.pe/sunedu")
                    }
                });

                //Configura la seguridad a todas las clases y metodos que tengan configurado [Authorize]
                //Se implementa en la clase [/Infraestructura/AuthorizeCheckOperationFilter.cs]
                //options.OperationFilter<AuthorizeCheckOperationFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.EnableAnnotations();

                //options.SchemaFilter<SnakeCaseSchemaFilter>();

            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}
