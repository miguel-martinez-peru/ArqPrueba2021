using AuthZ.BackgroundTask.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace AuthZ.BackgroundTask.Services
{
    public abstract class CustomBackgroundService : BackgroundService
    {
        protected CustomBgSetting _options;

        protected bool ExecuteDatosRolesTask(IConfiguration configuration)
        {
            bool ejecutar = false;

            string dateFormat = "HH:mm:ss";
            string date = DateTime.Now.ToString(dateFormat);

            var rangeTime = _options.ServiceRoles.ExecTime;

            if (date == rangeTime)
            {
                ejecutar = true;
            }

            return ejecutar;            
        }

        protected bool ExecuteDatosAplicacionesTask(IConfiguration configuration)
        {
            bool ejecutar = false;

            string dateFormat = "HH:mm:ss";
            string date = DateTime.Now.ToString(dateFormat);

            var rangeTime = _options.ServiceAplicaciones.ExecTime;

            if (date == rangeTime)
            {
                ejecutar = true;
            }

            return ejecutar;
        }

    }
}
