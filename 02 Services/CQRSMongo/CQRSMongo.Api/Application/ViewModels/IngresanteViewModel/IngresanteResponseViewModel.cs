using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.ViewModels.IngresanteModel
{
    public class IngresanteResponseDto
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
        public string CodigoFilial { get; set; }
        public string EsSedePrincipal { get; set; }
        public string CodigoUbigeoIneiFilial { get; set; }
        public string DepartamentoFilial { get; set; }
        public string ProvinciaFilial { get; set; }
        public string EstadoVigenciaFilial { get; set; }
        public DateTime? FechaVigenciaInicioFilial { get; set; }
        public DateTime? FechaVigenciaFinFilial { get; set; }
        public int? CodigoFacultadUnidad { get; set; }
        public string TipoUnidad { get; set; }
        public string NambreUnidad { get; set; }
        public string EstadoVigenciaUnidad { get; set; }
        public DateTime? FechaVigenciaInicioUnidad { get; set; }
        public DateTime? FechaVigenciaFinUnidad { get; set; }
        public int? CodigoPrograma { get; set; }
        public string CodigoClasificadorCime { get; set; }
        public string ClasificadorCime { get; set; }
        public string NombrePrograma { get; set; }
        public string TipoAutorizacionPrograma { get; set; }
        public string TipoNivelAcademico { get; set; }
        public string NivelAcademico { get; set; }
        public string EstadoVigenciaPrograma { get; set; }
        public DateTime? FechaVigenciaInicioPrograma { get; set; }
        public DateTime? FechaVigenciaFinPrograma { get; set; }
        public string TipoProcesoAdmision { get; set; }
        public string ProcesoAdmision { get; set; }
        public int? Anio { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string TipoIngresante { get; set; }
        public string ModalidadIngreso { get; set; }
        public string ModalidadEstudio { get; set; }
        public Guid? GuidPersona { get; set; }
        public Guid? GuidIngresante { get; set; }
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
