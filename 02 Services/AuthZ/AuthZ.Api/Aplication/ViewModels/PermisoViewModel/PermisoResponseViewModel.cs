﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.Api.Application.ViewModels.PermisoViewModel
{
    public class PermisoResponseViewModel
    {
        public int IdPermiso { get; set; }
        public Guid IdSistema { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

}
