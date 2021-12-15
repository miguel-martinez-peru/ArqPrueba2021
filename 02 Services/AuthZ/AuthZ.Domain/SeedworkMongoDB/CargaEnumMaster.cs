using System.Collections.Generic;
using System.Linq;

namespace Soporte.Domain.SeedworkMongoDB
{
    public enum TipoCarga
    {
        NO_DEFINIDO = 0,
        DATOS_INGRESANTES = 1,
        DATOS_MATRICULADOS = 2,
        OCURRENCIAS_ACADEMICAS = 3,
        MODIFICACION_DATOS = 4,
        ACTUALIZACION_DATOS_PERSONAS = 5,
        DATOS_PERSONAS = 6,
        CURSOS = 7,
        MALLA_CURRICULAR = 19,
        DOCENTES = 9,
        PERIODO_DOCENTE = 10,
        OCURRENCIA_DOCENTE = 11,
        HORARIOS = 13,
        OCURRENCIA_CURSO = 14,
        MATRICULADO_CURSO = 15,
        CONVALIDADO_CURSO_EXTRANJERA = 16,
        CONVALIDADO_CURSO_NACIONAL = 17,
        CONVALIDADO_CURSO_INTERNA = 18,
        PERSONAL_ADMINISTRATIVO = 20,
        DATOS_COMPLEMENTARIOS_INGRESANTES = 21,
        CORRECCION_DATOS_DOCENTES = 22
    }

    public enum EstadoCarga
    {
        NO_DEFINIDO = 0,
        PENDIENTE = 10,
        EN_EVALUACION = 20,
        EVALUADO_CON_OBSERVACIONES = 21,
        EVALUADO_CORRECTO = 22,
        EN_REGISTRO = 30,
        REGISTRADO_CON_ERRORES = 31,
        REGISTRADO_CORRECTO = 32,
        FINALIZADO = 40,
        CANCELADO = 50
    }
}
