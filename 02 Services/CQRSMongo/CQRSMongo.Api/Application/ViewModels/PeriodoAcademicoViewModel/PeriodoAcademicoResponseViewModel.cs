using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicoOds.Api.Application.ViewModels
{
    public class PeriodoAcademicoResponseDto
    {
        public string AnioNumero { get; set; }
        public DateTime? FechaInicioPeriodo { get; set; }
        public DateTime? FechaFinPeriodo { get; set; }
    }

}
