using CQRSSqlServer.Api.Application.Queries;
using Autofac;

namespace CQRSSqlServer.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule
         : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DocenteQueries(QueriesConnectionString))
                .As<IDocenteQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new CondicionLaboralQueries(QueriesConnectionString))
                .As<ICondicionLaboralQueries>()
                .InstancePerLifetimeScope();
        }
    }
}

