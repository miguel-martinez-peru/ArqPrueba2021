using System;
using System.Collections.Generic;
using MediatR;
using System.Runtime.Serialization;
using AuthZ.Api.Application.Commands;
using AuthZ.Api.Application.ViewModels;

namespace AuthZ.Api.Application.Commands
{
    [DataContract]
    public class EliminarRolPermisoCommand : AuditoriaCommand, IRequest<GenericResponseViewModel>
    {
        [DataMember] public int IdPermiso { get; set; }
        [DataMember] public Guid IdRol { get; set; }
    }
}
