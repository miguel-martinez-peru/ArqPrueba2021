using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSqlServer.Api.Application.ViewModels.DocenteModel
{
    public class DocenteRequestDto
    {
        public string CodigoEntidad { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int? HorasLectivasDesde { get; set; }
        public int? HorasLectivasHasta { get; set; }
        public string CodigoCondicionLaboral { get; set; }
        public string CodigoRegimenDedicacion { get; set; }
        public string CodigoEstadoDocente { get; set; }

        public DateTime? FechaRegistroDesde { get; set; }
        public DateTime? FechaRegistroHasta { get; set; }
    }
}
