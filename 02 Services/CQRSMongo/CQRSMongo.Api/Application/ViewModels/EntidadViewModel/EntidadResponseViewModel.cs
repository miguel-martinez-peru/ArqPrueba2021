using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.ViewModels
{
    public class EntidadResponseDto
    {
        public int? IdEntidad { get; set; }
        public string CodigoEntidad { get; set; }
        public string CodigoEntidadInei { get; set; }
        public string Nombre { get; set; }
        public string Siglas { get; set; }
    }

}
