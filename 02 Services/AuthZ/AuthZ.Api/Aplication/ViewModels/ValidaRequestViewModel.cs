using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.Api.Aplication.ViewModels
{
    public class ValidaRequestViewModel
    {
        public Guid rol { get; set; }
        public int permiso { get; set; }
    }
}
