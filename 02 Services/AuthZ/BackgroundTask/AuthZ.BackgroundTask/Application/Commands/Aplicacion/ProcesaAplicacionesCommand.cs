
using AuthZ.BackgroundTask.Application.ViewModels;
using AuthZ.BackgroundTask.Application.ViewModels.AplicacionViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

/// <summary>
/// Comando utilizado para crear un nuevo registro en la tabla  [maestro.entidad]
/// </summary>
namespace AuthZ.BackgroundTask.Application.Commands
{
    [DataContract]
    public class ProcesaAplicacionesCommand : IRequest<GenericResponseViewModel>
    {
        [DataMember] public List<AplicacionViewModel> Aplicaciones { get; set; }

    }
}

