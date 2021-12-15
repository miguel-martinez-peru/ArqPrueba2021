using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.ViewModels.DocenteModel
{
    public class DocenteResponseDto
    {
        public string CodigoEntidad { get; set; }
        public string CodigoIneiEntidad { get; set; }
        public string NombreEntidad { get; set; }
        public string TipoAutorizacionEntidad { get; set; }
        public string TipoEntidad { get; set; }
        public string TipoGestion { get; set; }
        public string CodigoUbigeoIneiEntidad { get; set; }
        public string DepartamentoEntidad { get; set; }
        public string ProvinciaEntidad { get; set; }
        public string DistritoEntidad { get; set; }
        public string EstadoVigenciaEntidad { get; set; }
        public DateTime? FechaVigenciaInicioEntidad { get; set; }
        public DateTime? FechaVigenciaFinEntidad { get; set; }
        public string TipoNivelAcademico { get; set; }
        public string NivelAcademico { get; set; }
        public string PeriodoAcademico { get; set; }
        public string PeriodoLectivo { get; set; }
        public int? Anio { get; set; }
        public string GradoAcademico { get; set; }
        public string CondicionLaboral { get; set; }
        public string RegimenDedicacion { get; set; }
        public string IdiomaDocente { get; set; }
        public string TipoIdiomaDocente { get; set; }
        public string EstadoDocente { get; set; }
        public string TipoActividadDocente { get; set; }
        public string ActividadDocente { get; set; }
        public int? HorasLectivasSemanales { get; set; }
        public int? HorasNoLectivasSemanales { get; set; }
        public Guid? GuidPersona { get; set; }
        public Guid? GuidDocente { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string TieneUnApellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string Sexo { get; set; }
        public string CodigoPaisNacimiento { get; set; }
        public string PaisNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string CodigoUbigeoIneiNacimiento { get; set; }
        public string DepartamentoNacimiento { get; set; }
        public string ProvinciaNacimiento { get; set; }
        public string DistritoNacimiento { get; set; }
        public string CodigoUbigeoIneiResidencia { get; set; }
        public string DepartamentoResidencia { get; set; }
        public string ProvinciaResidencia { get; set; }
        public string DistritoResidencia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

}
