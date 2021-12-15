using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.ViewModels.MatriculaModel
{
    public class MatriculaResponseDto
    {
        public string CodigoEntidad { get; set; }
        public string CodigoEntidadInei { get; set; }
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
        public string CodigoLocal { get; set; }
        public string EsLocalPrincipal { get; set; }
        public string DireccionLocal { get; set; }
        public string CodigoUbigeoIneiLocal { get; set; }
        public string DepartamentoLocal { get; set; }
        public string ProvinciaLocal { get; set; }
        public string DistritoLocal { get; set; }
        public string TipoAutorizacionLocal { get; set; }
        public string EstadoVigenciaLocal { get; set; }
        public int? CodigoFacultadUnidad { get; set; }
        public string TipoUnidad { get; set; }
        public string NombreUnidad { get; set; }
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
        public string PeriodoAcademico { get; set; }
        public string PeriodoLectivo { get; set; }
        public int? Anio { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public int? CicloAcademico { get; set; }
        public string ModalidadEstudio { get; set; }
        public string TieneCambioPrograma { get; set; }
        public DateTime? FechaCambioPrograma { get; set; }
        public string TieneBeca { get; set; }
        public string TipoBeca { get; set; }
        public decimal? PorcentajeCubiertoBeca { get; set; }
        public string CodigoEstudiante { get; set; }
        public Guid? GuidPersona { get; set; }
        public Guid? GuidIngresante { get; set; }
        public Guid? GuidMatriculado { get; set; }
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
        public string CodigoUbigeoNacimiento { get; set; }
        public string DepartamentoNacimiento { get; set; }
        public string ProvinciaNacimiento { get; set; }
        public string DistritoNacimiento { get; set; }
        public string CodigoUbigeoResidencia { get; set; }
        public string DepartamentoResidencia { get; set; }
        public string ProvinciaResidencia { get; set; }
        public string DistritoResidencia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

}
