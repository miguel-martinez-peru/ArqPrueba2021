using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.BackgroundTask.Application.ViewModels.AplicacionViewModel
{
    public class AplicacionViewModel
    {
        public Guid IdSistema { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
    }
}
