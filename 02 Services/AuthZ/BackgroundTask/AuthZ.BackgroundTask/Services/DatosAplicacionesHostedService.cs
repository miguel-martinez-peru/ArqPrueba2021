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
    public class DatosAplicacionesHostedService : CustomBackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IAplicacionQueries _aplicacionQueries;
        private readonly IMediator _mediator;

        public DatosAplicacionesHostedService(IOptions<CustomBgSetting> options,
            IConfiguration configuration,
            IMediator mediator,
            IAplicacionQueries aplicacionQueries)
        {
            _configuration = configuration;
            _mediator = mediator;
            _aplicacionQueries = aplicacionQueries ?? throw new ArgumentNullException(nameof(aplicacionQueries));
            base._options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {
            Log.Information($"Inicio de tarea ---DatosAplicacionesHostedService---");

            while (!stopToken.IsCancellationRequested)
            {
                try
                {
                    if (base.ExecuteDatosAplicacionesTask(_configuration))
                    {
                        Log.Information($"Ejecutando tarea --ExecuteDatosAplicacionesTask--");

                        Guid guidProccess = Guid.NewGuid();
                        Log.Information($"---Iniciando consultas--- Guid:{guidProccess}");

                        var dataOrigen = await _aplicacionQueries.ObtenerAplicaciones();
                        Log.Information($"Cargando información desde PUNKU --APLICACIONES--");

                        var command = new ProcesaAplicacionesCommand { Aplicaciones = dataOrigen };
                        var cltToken = new System.Threading.CancellationToken();
                        var commandResult = await _mediator.Send(command, cltToken);
                        Log.Information($"Enviando información hacia AuthZ --APLICACIONES--");

                        if (commandResult.HasError())
                            Log.Information($"Ocurrió un error en el envío.");
                        
                        Log.Information($"---Se envió con éxito: {command.Aplicaciones.Count} ---");

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
