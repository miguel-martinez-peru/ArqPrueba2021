using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.ViewModels.MatriculaModel
{
    public class MatriculaRequestDto
    {
        public string CodigoEntidad { get; set; }
        public string CodigoPeriodoLectivo { get; set; }
        public string CodigoNivelAcademico { get; set; }
        public string CodigoPeriodoAcademico { get; set; }
        public string CodigoModalidadEstudio { get; set; }
        public string CicloAcademico { get; set; }
        public string CodigoFilial { get; set; }
        public string CodigoLocal { get; set; }
        public string CodigoFacultad { get; set; }
        public string CodigoPrograma { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string CodigoEstudiante { get; set; }

        public DateTime? FechaRegistroDesde { get; set; }
        public DateTime? FechaRegistroHasta { get; set; }
    }
}
