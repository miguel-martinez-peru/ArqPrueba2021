using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.ViewModels.IngresanteModel
{
    public class IngresanteRequestDto
    {
        public string CodigoEntidad { get; set; }
        public string CodigoTipoIngresante { get; set; }
        public string CodigoProcesoAdmision { get; set; }
        public string CodigoNivelAcademico { get; set; }
        public string CodigoFilial { get; set; }
        public string CodigoFacultad { get; set; }
        public string CodigoPrograma { get; set; }
        public string CodigoModalidadIngreso { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }

        public DateTime? FechaRegistroDesde { get; set; }
        public DateTime? FechaRegistroHasta { get; set; }

    }
}
