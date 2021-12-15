using Core.SeedworkCommands;
using CQRSSqlServer.Domain.Events;
using CQRSSqlServer.Domain.Exceptions;
using CQRSSqlServer.Domain.Seedwork;
using System;
using System.Collections.Generic;

namespace CQRSSqlServer.Domain.AggregatesModel.LocalAggregate
{
    public class Local : Entity, IAggregateRoot
    {
        public int _IdLocal;
        public int _IdFilial;
        public string _Codigo;
        public int? _AforoLocal;
        public string _Comentarios;
        public int _IdTblEstadoVigencia;
        public bool _EsEliminado;
        public Guid _Guid;

        protected Local() { }

        public Local(
            int IdLocal
            , int IdFilial
            , string Codigo
            , int? AforoLocal
            , string Comentarios
            , int IdTblEstadoVigencia
            , bool EsEliminado
            , Guid Guid

            , Guid _usuarioCreacion
            , DateTime _fechaCreacion
            , string _ipCreacion
            ) : this()
        {
            GuidValue = Guid;
            _IdLocal = IdLocal;
            _IdFilial = IdFilial;
            _Codigo = Codigo;
            _AforoLocal = AforoLocal;
            _Comentarios = Comentarios;
            _IdTblEstadoVigencia = IdTblEstadoVigencia;
            _EsEliminado = EsEliminado;

            UsuarioCreacion = _usuarioCreacion;
            FechaCreacion = _fechaCreacion;
            IpCreacion = _ipCreacion;
        }

        public Local SetLocalDelete(
            int IdLocal
            , Guid? _usuarioModificacion
            , DateTime? _fechaModificacion
            , string _ipModificacion
            )
        {
            _IdLocal = IdLocal;
            _EsEliminado = true;

            UsuarioModificacion = _usuarioModificacion;
            FechaModificacion = _fechaModificacion;
            IpModificacion = _ipModificacion;

            return this;
        }

        public Local SetLocalUpdate(int IdLocal, string Codigo, bool EsLocalPrincipal, string Direccion,
            string Referencia, string OtroTipoServicio, decimal? AreaTerreno, string Telefono,
            decimal? AreaConstruida, int? AforoLocal, string Comentarios
            , Guid? _usuarioModificacion
            , DateTime? _fechaModificacion
            , string _ipModificacion)
        {
            if (string.IsNullOrWhiteSpace(_Codigo))
            {
                throw new DomainException("U01: Código es requerido");
            }

            _IdLocal = IdLocal;
            _Codigo = Codigo;
            _AforoLocal = AforoLocal;
            _Comentarios = Comentarios;

            //OPCIONAL: Registra un evento en el rabbit (parametros depende del negocio, esto es solo ejemplo)
            this.AddSeguimientoEstadoTablaCreacionDomainEvent(IdLocal, this._IdTblEstadoVigencia);

            UsuarioModificacion = _usuarioModificacion;
            FechaModificacion = _fechaModificacion;
            IpModificacion = _ipModificacion;

            return this;
        }


        #region Eventos del Dominio
        private void AddSeguimientoEstadoTablaCreacionDomainEvent(int idMaestraDetalle, int idMaestraDetalleAnterior = 0)
        {
            var evento = new SeguimientoEstadoTablaCreacionDomainEvent(0, this.Id, new DateTime(), null, false, true, new DateTime(), new Guid(), "");
            this.AddDomainEvent(evento);
        }
        #endregion

    }
}
