using CQRSSqlServer.Domain.AggregatesModel.LocalAggregate;
using CQRSSqlServer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Institucional.Infrastructure.EntityConfigurations
{
    class LocalEntityTypeConfiguration: IEntityTypeConfiguration<Local>
    {
        public void Configure(EntityTypeBuilder<Local> localConfiguration)
        {
            localConfiguration.ToTable("local", Schema.Maestro);
            localConfiguration.Ignore(b => b.DomainEvents);
            localConfiguration.HasKey(x => x.Id);

            localConfiguration.Property(x => x.Id).HasColumnName("ID_LOCAL").ValueGeneratedOnAdd();
            localConfiguration.Property<int>("_IdFilial").HasColumnName("ID_FILIAL").IsRequired();
            localConfiguration.Property<string>("_Codigo").HasColumnName("CODIGO").IsRequired();
            localConfiguration.Property<bool>("_EsLocalPrincipal").HasColumnName("ES_LOCAL_PRINCIPAL").IsRequired();
            localConfiguration.Property<int>("_IdUbigeo").HasColumnName("ID_UBIGEO").IsRequired();

            localConfiguration.Property(x => x.FechaCreacion).HasColumnName("FECHA_CREACION").IsRequired();
            localConfiguration.Property(x => x.UsuarioCreacion).HasColumnName("USUARIO_CREACION").IsRequired();
            localConfiguration.Property(x => x.IpCreacion).HasColumnName("IP_CREACION").IsRequired();
            localConfiguration.Property(x => x.UsuarioModificacion).HasColumnName("USUARIO_MODIFICACION").IsRequired(false);
            localConfiguration.Property(x => x.FechaModificacion).HasColumnName("FECHA_MODIFICACION").IsRequired(false);
            localConfiguration.Property(x => x.IpModificacion).HasColumnName("IP_MODIFICACION").IsRequired(false);


        }
    }
}
