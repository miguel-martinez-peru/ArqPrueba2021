using AuthZ.BackgroundTask.Application.Commands;
using AuthZ.BackgroundTask.Application.Queries;
using AuthZ.BackgroundTask.Config;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthZ.BackgroundTask.Services
{
    public class DatosRolesHostedService : CustomBackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IRolQueries _rolQueries;
        private readonly IMediator _mediator;

        public DatosRolesHostedService(IOptions<CustomBgSetting> options,
            IConfiguration configuration,
            IMediator mediator,
            IRolQueries rolQueries)
        {
            _configuration = configuration;
            _mediator = mediator;
            _rolQueries = rolQueries ?? throw new ArgumentNullException(nameof(rolQueries)); 
            base._options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {
            Log.Information($"Inicio de tarea ---DatosRolesHostedService---");

            while (!stopToken.IsCancellationRequested)
            {
                try
                {
                    if (base.ExecuteDatosRolesTask(_configuration))
                    {
                        Log.Information($"Ejecutando tarea --ExecuteDatosRolesTask--");

                        Guid guidProccess = Guid.NewGuid();
                        Log.Information($"---Iniciando consultas--- Guid:{guidProccess}");

                        var dataOrigen = await _rolQueries.ObtenerRoles();
                        Log.Information($"Cargando información desde PUNKU --ROLES--");

                        var command = new ProcesaRolesCommand { Roles = dataOrigen };
                        var cltToken = new System.Threading.CancellationToken();
                        var commandResult = await _mediator.Send(command, cltToken);
                        Log.Information($"Enviando información hacia AuthZ --ROLES--");

                        if (commandResult.HasError())
                            Log.Information($"Ocurrió un error en el envío.");
                        
                        Log.Information($"---Se envió con éxito: {command.Roles.Count} ---");

                        Log.Information($"Terminando tarea --{Program.AppName}--");
                    }
                }
                catch (Exception e)
                {
                    Log.Information($"Exception --{e}--");
                }

            }

            Log.Information($"Fin de tarea ---DatosAcademicosHostedService---");
        }

    }
}
