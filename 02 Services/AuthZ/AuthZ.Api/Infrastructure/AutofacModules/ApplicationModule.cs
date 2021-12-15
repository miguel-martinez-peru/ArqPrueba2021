using AuthZ.Api.Aplication.Queries;
using AuthZ.Api.Application.Queries.Permiso;
using AuthZ.Api.Application.Queries.RolPermiso;
using AuthZ.Domain.AggregatesModel;
using AuthZ.Infrastructure.MongoDbRepositories;
using Autofac;
using System.Reflection;

namespace AuthZ.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule
          : Autofac.Module
    {

        public string QueriesConnectionString { get; }
        public string QueriesSuffixNameBd { get; }

        public ApplicationModule(string qsfnamedb, string qconstr)
        {
            QueriesSuffixNameBd = qsfnamedb;
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            #region Querys
            builder.RegisterType<RolPermisoQueries>()
                .As<IRolPermisoQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PermisoQueries>()
                .As<IPermisoQueries>()
                .InstancePerLifetimeScope();        

            #endregion

            #region Repositorios
            //builder.RegisterType<ArchivoRepository>()
            //     .As<IArchivoRepository>()
            //     .InstancePerLifetimeScope();

            //builder.RegisterType<ServicioIngresanteRepository>()
            //     .As<IServicioIngresanteRepository>()
            //     .InstancePerLifetimeScope();
            #endregion

            #region Repositorios MongoDB
            builder.RegisterType<MongoDBGenericRepository>()
               .As<IMongoDBGenericRepository>()
               .InstancePerLifetimeScope();
            #endregion

            #region  Cache
            builder.RegisterType<CacheManager>()
               .AsSelf();
            #endregion

            //builder.RegisterAssemblyTypes(typeof(CrearServicioIngresanteIntegrationEventHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
