using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.SeedworkCommands;
using CQRSSqlServer.Domain.AggregatesModel.LocalAggregate;
using Institucional.Infrastructure.EntityConfigurations;

namespace CQRSSqlServer.Infrastructure
{
    public class InstitucionalContext : DbContext, IUnitOfWork
    {
        private InstitucionalContext(DbContextOptions<InstitucionalContext> options) : base(options) { }

        public InstitucionalContext(DbContextOptions<InstitucionalContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine("InstitucionalContext::ctor ->" + this.GetHashCode());
        }


        //TABLAS
        public DbSet<Local> Locales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LocalEntityTypeConfiguration());
        }

        //MEDIATR
        private readonly IMediator _mediator;

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync();
            return true;
        }
    }






}

