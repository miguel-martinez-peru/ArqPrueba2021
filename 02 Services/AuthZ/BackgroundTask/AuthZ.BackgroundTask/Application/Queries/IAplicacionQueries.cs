using AuthZ.BackgroundTask.Application.ViewModels.AplicacionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.BackgroundTask.Application.Queries
{
    public interface IAplicacionQueries
    {
        Task<List<AplicacionViewModel>> ObtenerAplicaciones();
    }
}
