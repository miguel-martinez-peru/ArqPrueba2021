using AuthZ.BackgroundTask.Application.ViewModels.RolViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.BackgroundTask.Application.Queries
{
    public interface IRolQueries
    {
        Task<List<RolViewModel>> ObtenerRoles();
    }
}
