using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuthZ.Cliente
{
    public enum PermisosEnum : short
    {
        NotSet = 0, //error condition

        //1000  ACADEMICO
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Academico_PuedeConsultar = 1001,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Academico_PuedeConsultarGlobal = 1002,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Academico_PuedeModificar = 1003,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Academico_PuedeAprobarAmpliacion = 1004,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Academico_PuedeGenerarPdfDeAmpliación = 1005,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Academico_PuedeAprobarCorreccion = 1006,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Academico_PuedeGenerarPdfDeCorreccion = 1007,

        //2000  CARNE
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeConsultar = 2001,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeModificar = 2002,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeRecepcionarActaDeDespacho = 2003,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeNotificarActaDeDespacho = 2004,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeGenerarActaDeEntrega = 2005,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeValidarBiometria = 2006,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeDescargarVoucherDePago = 2007,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeDescagarFacturaDePago = 2008,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeProcesarProduccion = 2009,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeNotificarProduccion = 2010,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeNotificarResponsableDeRecojo = 2011,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeProcesarSolicitudMasiva = 2012,
        [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
        Carne_PuedeNotificarDevolucion = 2013,

        //3000  CONFIGURACION

        //4000  CURRICULA

        //5000  DOCENTE
        [Display(GroupName = "Docente", Name = "PuedeConsultar", Description = "Puede consultar")]
        Docente_PuedeConsultar = 5001,
        [Display(GroupName = "Docente", Name = "PuedeModificar", Description = "Puede modificar")]
        Docente_PuedeModificar = 5002,
        [Display(GroupName = "Docente", Name = "PuedeValidarPeriodoAcademicoDeEntidad", Description = "Puede validar el periodo académico de una entidad")]
        Docente_PuedeValidarPeriodoAcademicoDeEntidad = 5003,
        [Display(GroupName = "Docente", Name = "PuedeModificarGlobal", Description = "Puede modificar globalmente")]
        Docente_PuedeModificarGlobal = 5004,
        [Display(GroupName = "Docente", Name = "PuedeProcesarServicios", Description = "Puede procesar servicios de docente")]
        Docente_PuedeProcesarServicios = 5005,
        [Display(GroupName = "Docente", Name = "PuedeCargarCondenasDePersona", Description = "Puede cargar condenas de persona")]
        Docente_PuedeCargarCondenasDePersona = 5006,

        //6000  INGRESANTE

        //7000  INSTITUCIONAL

        //8000  MATRICULA

        //9000  PERSONA

        //10000 REPORTE

        //11000 SOPORTE

        //12000 TRANSVERSAL

        //13000 GATEWAY UNIVERSIDAD
        [Display(GroupName = "Maestro", Name = "PuedeConsultar", Description = "Puede consultar los maestros")]
        Maestro_PuedeConsultar = 13001,
        [Display(GroupName = "Postulante", Name = "PuedeConsultar", Description = "Puede ")]
        Postulante_PuedeConsultar = 13002,
        [Display(GroupName = "Postulante", Name = "PuedeModificar", Description = "Puede ")]
        Postulante_PuedeModificar = 13003,
        [Display(GroupName = "Postulante", Name = "PuedeMasivo", Description = "Puede ")]
        Postulante_PuedeMasivo = 13004,
        [Display(GroupName = "Postulante", Name = "PuedeConsultarGeneral", Description = "Puede ")]
        Postulante_PuedeConsultarGeneral = 13005,
        [Display(GroupName = "Egresado", Name = "PuedeConsultar", Description = "Puede ")]
        Egresado_PuedeConsultar = 13006,
        [Display(GroupName = "Egresado", Name = "PuedeModificar", Description = "Puede ")]
        Egresado_PuedeModificar = 13007,
        [Display(GroupName = "Egresado", Name = "PuedeMasivo", Description = "Puede ")]
        Egresado_PuedeMasivo = 13008,
        [Display(GroupName = "Egresado", Name = "PuedeConsultarGeneral", Description = "Puede ")]
        Egresado_PuedeConsultarGeneral = 13009,
        [Display(GroupName = "Ingresante", Name = "PuedeConsultar", Description = "Puede ")]
        Ingresante_PuedeConsultar = 13010,
        [Display(GroupName = "Ingresante", Name = "PuedeModificar", Description = "Puede ")]
        Ingresante_PuedeModificar = 13011,
        [Display(GroupName = "Ingresante", Name = "PuedeMasivo", Description = "Puede ")]
        Ingresante_PuedeMasivo = 13012,
        [Display(GroupName = "Ingresante", Name = "PuedeConsultarGeneral", Description = "Puede ")]
        Ingresante_PuedeConsultarGeneral = 13013,
        [Display(GroupName = "Matricula", Name = "PuedeConsultar", Description = "Puede ")]
        Matricula_PuedeConsultar = 13014,
        [Display(GroupName = "Matricula", Name = "PuedeModificar", Description = "Puede ")]
        Matricula_PuedeModificar = 13015,
        [Display(GroupName = "Matricula", Name = "PuedeMasivo", Description = "Puede ")]
        Matricula_PuedeMasivo = 13016,
        [Display(GroupName = "Matricula", Name = "PuedeConsultarGeneral", Description = "Puede ")]
        Matricula_PuedeConsultarGeneral = 13017,
        [Display(GroupName = "OcurrenciaAcademica", Name = "PuedeMasivo", Description = "Puede ")]
        OcurrenciaAcademica_PuedeMasivo = 13018,
        [Display(GroupName = "PeriodoAcademico", Name = "PuedeConsultar", Description = "Puede ")]
        PeriodoAcademico_PuedeConsultar = 13019,
        [Display(GroupName = "ProcesoAdmision", Name = "PuedeConsultar", Description = "Puede ")]
        ProcesoAdmision_PuedeConsultar = 13020,
        [Display(GroupName = "SolicitudAmpliacion", Name = "PuedeConsultar", Description = "Puede ")]
        SolicitudAmpliacion_PuedeConsultar = 13021,
        [Display(GroupName = "SolicitudAmpliacion", Name = "PuedeRegistrar", Description = "Puede ")]
        SolicitudAmpliacion_PuedeRegistrar = 13022,
        [Display(GroupName = "SolicitudEliminación", Name = "PuedeConsultar", Description = "Puede ")]
        SolicitudEliminación_PuedeConsultar = 13023,
        [Display(GroupName = "SolicitudEliminación", Name = "SolicitudEliminación", Description = "Puede ")]
        SolicitudEliminación_PuedeRegistrar = 13024,
        [Display(GroupName = "EstadoCarga", Name = "PuedeConsultar", Description = "Puede ")]
        EstadoCarga_PuedeConsultar = 13025,
        [Display(GroupName = "TrazabilidadAcademica", Name = "PuedeConsultar", Description = "Puede ")]
        TrazabilidadAcademica_PuedeConsultar = 13026,
        [Display(GroupName = "ValidacionReniec", Name = "PuedeConsultar", Description = "Puede ")]
        ValidacionReniec_PuedeConsultar = 13027,
        [Display(GroupName = "ValidacionReniec", Name = "PuedeCorregir", Description = "Puede ")]
        ValidacionReniec_PuedeCorregir = 13028,
        [Display(GroupName = "EstructuraVacante", Name = "PuedeConsultar", Description = "Puede ")]
        EstructuraVacante_PuedeConsultar = 13029,
        [Display(GroupName = "EstructuraVacante", Name = "PuedeRegistrar", Description = "Puede ")]
        EstructuraVacante_PuedeRegistrar = 13030,
        [Display(GroupName = "EstructuraVacante", Name = "PuedeModificar", Description = "Puede ")]
        EstructuraVacante_PuedeModificar = 13031,

        [Display(GroupName = "Docente gateway", Name = "PuedeConsultar", Description = "Puede ")]
        DocenteGw_PuedeConsultar = 13032,
        [Display(GroupName = "Docente gateway", Name = "PuedeModificar", Description = "Puede ")]
        DocenteGw_PuedeModificar = 13033,
        [Display(GroupName = "Docente gateway", Name = "PuedeMasivo", Description = "Puede ")]
        DocenteGw_PuedeMasivo = 13034,
        [Display(GroupName = "Docente gateway", Name = "PuedeConsultarGeneral", Description = "Puede ")]
        DocenteGw_PuedeConsultarGeneral = 13035,
        [Display(GroupName = "ProcesoAdmision", Name = "PuedeRegistrar", Description = "Puede ")]
        ProcesoAdmision_PuedeRegistrar = 13036,
        [Display(GroupName = "ProcesoAdmision", Name = "PuedeModificar", Description = "Puede ")]
        ProcesoAdmision_PuedeModificar = 13037,

        [Display(GroupName = "DocumentoNormativo gateway", Name = "PuedeConsultar", Description = "Puede ")]
        DocumentoNormativoGw_PuedeConsultar = 13038,
        [Display(GroupName = "DocumentoNormativo gateway", Name = "PuedeRegistrar", Description = "Puede ")]
        DocumentoNormativoGw_PuedeRegistrar = 13039,



        //This is an example of what to do with permission you don't used anymore.
        //You don't want its number to be reused as it could cause problems 
        //Just mark it as obsolete and the PermissionDisplay code won't show it
        [Obsolete]
        [Display(GroupName = "Old", Name = "Not used", Description = "example of old permission")]
        OldPermissionNotUsed = 100,

        [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
        AccessAll = Int16.MaxValue,
    }
}
