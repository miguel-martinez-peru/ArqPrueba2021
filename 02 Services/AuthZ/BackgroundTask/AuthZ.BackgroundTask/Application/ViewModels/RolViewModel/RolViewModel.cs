using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.BackgroundTask.Application.ViewModels.RolViewModel
{
    public class RolViewModel
    {
        public Guid IdRol { get; set; }
        public int IdSistema { get; set; }
        public string Nombre { get; set; }
    }
}
