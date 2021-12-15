using CQRSSqlServer.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSSqlServer.Api.Application.DomainEventHandlers.SeguimientoEstadoTablaCreacion
{
    public class SeguimientoEstadoTablaCreacionDomainEventHandler : INotificationHandler<SeguimientoEstadoTablaCreacionDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        //private readonly ISeguimientoEstadoTablaRepository _seguimientoEstadoTablaRepository;

        public SeguimientoEstadoTablaCreacionDomainEventHandler(ILoggerFactory logger
            //, ISeguimientoEstadoTablaRepository seguimientoEstadoTablaRepository
            )
        {
            _logger = logger;
            //_seguimientoEstadoTablaRepository = seguimientoEstadoTablaRepository;
        }

        public async Task Handle(SeguimientoEstadoTablaCreacionDomainEvent notification, CancellationToken cancellationToken)
        {
            if (!notification.EsEjecutado)
            {
                //var seguimientoEstadoTablaNuevo = new SeguimientoEstadoTabla(0, notification.IdRegistro, notification.IdTblTablaOrigen, notification.IdTblMaestra, notification.IdTblMaestraDetalleAnterior, notification.FechaEstadoInicioAnterior, null, notification.UsuarioCreacion, DateTime.Now, notification.IpCreacion, true);
                //_seguimientoEstadoTablaRepository.Add(seguimientoEstadoTablaNuevo);
                //await _seguimientoEstadoTablaRepository.UnitOfWork.SaveChangesAsync();

                notification.SetEjecutado();

                #region calling integration event

                /*
                var entidadCambiarEstadoIntegrationEvent = new EntidadCambiarEstadoEvent(notificationEvent.Entidad.Id, notificationEvent.EsEliminado);
                await _entidadIntegrationEventService.PublishThroughEventBusAsync(entidadCambiarEstadoIntegrationEvent);

                _logger.CreateLogger(nameof(EntidadCambiarEstadoHistoricoDomainEventHandler)).LogTrace($"Entidad {entidadActualizada.Id} actualizada evento enviado  : {notificationEvent.Entidad.Id}.");
                */

                #endregion

            }

        }


    }
}
