using Core;
using Core.Auditoria;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Institucional.Api.Application.Commands
{
    public class CrearLocalCommand: Auditoria, IRequest<GenericResult>
    {
        public int IdLocal { get; set; }
        public int IdFilial { get; set; }
        public string Codigo { get; set; }
        public int? AforoLocal { get; set; }
        public string Comentarios { get; set; }
        public int IdTblEstadoVigencia { get; set; }
        public Guid Guid { get; set; }
        public bool EsEliminado { get; set; }

        public CrearLocalCommand(
        int idLocal,
        int idFilial,
        string codigo,
        int? aforoLocal,
        string comentarios,
        int idTblEstadoVigencia,
        Guid guid,
        bool esEliminado
            )
        {
            Guid = guid;
            IdLocal = idLocal;
            IdFilial = idFilial;
            Codigo = codigo;
            AforoLocal = aforoLocal;
            Comentarios = comentarios;
            IdTblEstadoVigencia = idTblEstadoVigencia;
            EsEliminado = esEliminado;
        }
    }
}
