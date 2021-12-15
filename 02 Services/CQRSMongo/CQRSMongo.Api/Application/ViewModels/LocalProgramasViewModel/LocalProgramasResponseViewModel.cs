using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.ViewModels
{
    public class EntidadFilialResponseDto
    {
        public string CodigoFilial { get; set; }
        public string Descripcion { get; set; }
    }

    public class EntidadLocalResponseDto
    {
        public string CodigoLocal { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string CodigoFilial { get; set; }
    }

    public class EntidadFacultadResponseDto
    {
        public int? CodigoFacultad { get; set; }
        public string Descripcion { get; set; }
    }

    public class EntidadProgramaResponseDto
    {
        public int? CodigoPrograma { get; set; }
        public string Descripcion { get; set; }
        public int? CodigoFacultad { get; set; }
    }

}
