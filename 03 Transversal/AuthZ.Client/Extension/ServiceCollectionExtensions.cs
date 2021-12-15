using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using AuthZ.Cliente.Punku;
using AuthZ.Cliente.Cabecera;

namespace AuthZ.Cliente
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomPunkuAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var baseuri = configuration.GetValue<string>("Auth:AuthZ");

            services.AddHttpContextAccessor();
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPunkuPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            services.AddHttpClient(name: "AuthZ", configureClient: client =>
            {
                client.BaseAddress = new Uri(baseuri);
            });

            return services;
        }

        public static IServiceCollection AddCustomHeaderAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationHeaderPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermisoHeaderHandler>();

            var baseuri = configuration.GetValue<string>("Auth:AuthZ");
            services.AddHttpClient(name: "AuthZ", configureClient: client =>
            {
                client.BaseAddress = new Uri(baseuri);
            });

            return services;
        }
    }
}
