
using AuthZ.Api.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

/// <summary>
/// Comando utilizado para crear un nuevo registro en la tabla  [maestro.entidad]
/// </summary>
namespace AuthZ.Api.Application.Commands
{
    [DataContract]
    public class CrearPermisoCommand : AuditoriaCommand, IRequest<GenericResponseViewModel>
    {
        [DataMember] public int IdPermiso { get; set; }
        [DataMember] public Guid IdSistema { get; set; }
        [DataMember] public string Codigo { get; set; }
        [DataMember] public string Nombre { get; set; }
        [DataMember] public string Descripcion { get; set; }
        [DataMember] public bool EsActivo { get; set; }

    }
}

