using AcademicoOds.Api.Application.Queries;
using Autofac;

namespace AcademicoOds.Api.Infrastructure.AutofacModules
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
            builder.Register(c => new IngresanteQueries(QueriesConnectionString))
                .As<IIngresanteQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new MatriculaQueries(QueriesConnectionString))
                .As<IMatriculaQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new DocenteQueries(QueriesConnectionString))
                .As<IDocenteQueries>()
                .InstancePerLifetimeScope();





            builder.Register(c => new CondicionLaboralQueries(QueriesConnectionString))
                .As<ICondicionLaboralQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new EntidadQueries(QueriesConnectionString))
                .As<IEntidadQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new LocalProgramasQueries(QueriesConnectionString))
                .As<ILocalProgramasQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new ModalidadEstudioQueries(QueriesConnectionString))
                .As<IModalidadEstudioQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new ModalidadIngresoQueries(QueriesConnectionString))
                .As<IModalidadIngresoQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new NivelAcademicoQueries(QueriesConnectionString))
                .As<INivelAcademicoQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new PeriodoAcademicoQueries(QueriesConnectionString))
                .As<IPeriodoAcademicoQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new PeriodoLectivoQueries(QueriesConnectionString))
                .As<IPeriodoLectivoQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new ProcesoAdmisionQueries(QueriesConnectionString))
                .As<IProcesoAdmisionQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new RegimenDedicacionQueries(QueriesConnectionString))
                .As<IRegimenDedicacionQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new TipoDocumentoQueries(QueriesConnectionString))
                .As<ITipoDocumentoQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new TipoIngresanteQueries(QueriesConnectionString))
                .As<ITipoIngresanteQueries>()
                .InstancePerLifetimeScope();
        }
    }
}

