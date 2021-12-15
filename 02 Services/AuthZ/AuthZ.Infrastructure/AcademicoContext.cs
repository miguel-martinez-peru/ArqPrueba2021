using Academico.Domain.AggregatesModel.CambioProgramaAggregate;
using Academico.Domain.AggregatesModel.CronogramaRegistroDocenteAggregate;
using Academico.Domain.AggregatesModel.DatosIngresoAggregate;
using Academico.Domain.AggregatesModel.EscalaPagoAggregate;
using Academico.Domain.AggregatesModel.EstructuraVacanteAggregate;
using Academico.Domain.AggregatesModel.EstudianteAggregate;
using Academico.Domain.AggregatesModel.MatriculaAggregate;
using Academico.Domain.AggregatesModel.PeriodoAcademicoAggregate;
using Academico.Domain.AggregatesModel.ProcesoAdmisionAggregate;
using Academico.Domain.AggregatesModel.SolicitudCorrecionAggregate;
using Academico.Domain.SeedWork;
using Academico.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AuthZ.Infrastructure
{
    public class AcademicoContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "maestro";
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
               
        public DbSet<Programa> Programas { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        //public DbSet<CambioPrograma> CambioProgramas { get; set; }
        public DbSet<PeriodoAcademico> PeriodoAcademicos { get; set; }
        public DbSet<ProcesoAdmision> ProcesosAdmision { get; set; }
        public DbSet<DatosIngreso> DatosIngreso { get; set; }

        public DbSet<DatosIngresoComplementarios> DatosIngresantesComplementarios { get; set; }
        public DbSet<DatosIngresoIdioma> DatosIngresoIdiomas { get; set; }
        public DbSet<DatosIngresoLenguaNativa> DatosIngresoLenguaNativas { get; set; }

        public DbSet<VacanteNivel> VacantesNivel { get; set; }
        public DbSet<VacanteModalidad> VacantesModalidad { get; set; }
        public DbSet<EstructuraVacante> EstructuraVacantes { get; set; }
        public DbSet<EstructuraVacanteDetalle> EstructuraVacantesDetalle { get; set; }
        public DbSet<EscalaPago> EscalaPago { get; set; }
        public DbSet<EscalaPagoGrupo> EscalaPagoGrupo { get; set; }

        public DbSet<EscalaPagoNivel> EscalaPagoNivel { get; set; }
        public DbSet<EscalaPagoPrograma> EscalaPagoPrograma { get; set; }

        public DbSet<CronogramaRegistroDocente> CronogramaRegistroDocente { get; set; }
        public DbSet<CronogramaRegistroDocenteDetalle> CronogramaRegistroDocenteDetalle { get; set; }
        public DbSet<SolicitudCorreccionFoto> SolicitudCorrecionFoto { get; set; }
        


        private AcademicoContext(DbContextOptions<AcademicoContext> options) : base(options) { }

        public AcademicoContext(DbContextOptions<AcademicoContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            
            System.Diagnostics.Debug.WriteLine("AcademicoContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ProgramaEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {      

            // Despacho de la colección de eventos de dominio.
            // Opciones:
            // A) ANTES de hacer comit los datos en la DB (EF SaveChanges)  realizará una sola transacción, incluida
            // efectos secundarios de los controladores de eventos de dominio que usan el mismo DbContext con "InstancePerLifetimeScope" o "scoped" siempre
            // B) DESPUÉS de enviar datos en la DB (EF SaveChanges) se realizarán varias transacciones.
            // Tendrá que manejar la coherencia final y las acciones compensatorias en caso de fallas en cualquiera de los Manejadores.
            await _mediator.DispatchDomainEventsAsync(this);

            // Después de ejecutar esta línea todos los cambios (desde el Controlador de comandos y los Controladores de eventos de dominio)
            // realizado a través del DbContext se comprometerá (concretará, comitiará)
            var result = await base.SaveChangesAsync();

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"La Transacción {transaction.TransactionId} no es la actual");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }

    public static class Schema
    {
        public static string Maestro = "maestro";
        public static string Transaccional = "transaccional";
        public static string Sistema = "sistema";
        public static string Seguridad = "seguridad";
    }
}
