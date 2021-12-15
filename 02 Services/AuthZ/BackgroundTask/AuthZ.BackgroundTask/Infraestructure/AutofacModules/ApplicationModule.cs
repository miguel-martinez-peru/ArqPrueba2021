using AuthZ.BackgroundTask.Application.Queries;
using AuthZ.Domain.AggregatesModel;
using AuthZ.Infrastructure.MongoDbRepositories;
using Autofac;

namespace AuthZ.BackgroundTask.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {
            #region Queries  
            builder.Register(c => new RolQueries(QueriesConnectionString))
                .As<IRolQueries>()
                .InstancePerLifetimeScope();
            builder.Register(c => new AplicacionQueries(QueriesConnectionString))
                .As<IAplicacionQueries>()
                .InstancePerLifetimeScope();
            #endregion

            #region Repository
            #endregion

            #region Commands            
            #endregion

            #region IntegrationEvents              
            #endregion

            #region Repositorios MongoDB
            builder.RegisterType<MongoDBGenericRepository>()
               .As<IMongoDBGenericRepository>()
               .InstancePerLifetimeScope();
            #endregion
        }
    }
}
