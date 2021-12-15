using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.Api.Application.ViewModels.RolPermisoViewModel
{
    public class RolPermisoResponseViewModel
    {
        public int IdPermiso { get; set; }
        public Guid IdRol { get; set; }
        public bool EsEliminado { get; set; }
    }

}
