using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AuthZ.Api.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        //public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var hcBuilder = services.AddHealthChecks();

        //    hcBuilder
        //        .AddCheck("self", () => HealthCheckResult.Healthy());
        //        //.AddMongoDb()
        //        //.AddSqlServer(
        //        //    configuration["ConnectionString"],
        //        //    name: "SoporteDb-check",
        //        //    tags: new string[] { "soporteDb" });

        //    return services;
        //}

        //public static IApplicationBuilder UseCustomHealthChecks(this IApplicationBuilder app)
        //{
        //    app.UseHealthChecks("/liveness", new HealthCheckOptions
        //    {
        //        Predicate = r => r.Name.Contains("self")
        //    });

        //    app.UseHealthChecks("/hc", new HealthCheckOptions()
        //    {
        //        Predicate = _ => true,
        //        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        //    });

        //    return app;
        //}
    }
}
