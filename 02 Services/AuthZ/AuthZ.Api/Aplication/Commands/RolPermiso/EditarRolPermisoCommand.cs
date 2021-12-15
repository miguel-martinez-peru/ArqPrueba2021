using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AuthZ.Api.Application.Commands;
using AuthZ.Api.Application.ViewModels;

/// <summary>
/// Comando utilizado para crear un nuevo registro en la tabla  [maestro.entidad]
/// </summary>
namespace AuthZ.Api.Application.Commands
{
    [DataContract]
    public class EditarRolPermisoCommand : AuditoriaCommand, IRequest<GenericResponseViewModel>
    {
        [DataMember] public int IdPermiso { get; set; }
        [DataMember] public Guid IdRol { get; set; }
        [DataMember] public bool Eliminado { get; set; }

    }
}

