using Microsoft.AspNetCore.Hosting;
using System.ServiceProcess;
using AuthZ.BackgroundTask.Services;

namespace AuthZ.BackgroundTask.Extensions
{
    public static class WebHostServiceExtensions
    {        
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new CustomWebHostService(host);
            ServiceBase.Run(webHostService);
        }
        
    }
}
