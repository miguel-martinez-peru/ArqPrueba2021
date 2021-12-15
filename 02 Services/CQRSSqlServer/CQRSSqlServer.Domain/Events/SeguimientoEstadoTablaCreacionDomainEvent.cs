using MediatR;
using System;
using System.Collections.Generic;

namespace CQRSSqlServer.Domain.Events
{
    public class SeguimientoEstadoTablaCreacionDomainEvent : INotification
    {
        public int IdSeguimientoEstadoTabla { get; private set; }
        public int IdRegistro { get; private set; }
        public DateTime FechaEstadoInicio { get; private set; }
        public DateTime? FechaEstadoFin { get; private set; }
        public bool? EsEstadoInicial { get; private set; }
        public DateTime FechaEstadoInicioAnterior { get; private set; }
        public Guid UsuarioCreacion { get; private set; }
        public string IpCreacion { get; private set; }
        public bool EsEliminado { get; private set; }
        public bool EsEjecutado { get; private set; }

        public SeguimientoEstadoTablaCreacionDomainEvent(int idSeguimientoEstadoTabla, int idRegistro, DateTime fechaEstadoInicio,
                                                DateTime? fechaEstadoFin, bool esEliminado, bool? esEstadoInicial, DateTime fechaEstadoInicioAnterior,
                                                Guid usuarioCreacion, string ipCreacion)
        {
            IdSeguimientoEstadoTabla = idSeguimientoEstadoTabla;
            IdRegistro = idRegistro;
            FechaEstadoInicio = fechaEstadoInicio;
            FechaEstadoFin = fechaEstadoFin;
            EsEliminado = esEliminado;
            EsEstadoInicial = esEstadoInicial;
            FechaEstadoInicioAnterior = fechaEstadoInicioAnterior;
            UsuarioCreacion = usuarioCreacion;
            IpCreacion = ipCreacion;
        }

        public void SetEjecutado()
        {
            this.EsEjecutado = true;
        }
    }
}
