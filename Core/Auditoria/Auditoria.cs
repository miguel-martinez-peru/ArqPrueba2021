using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Auditoria
{
    public class Auditoria
    {
        public Guid UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string IpCreacion { get; set; }
        public Guid? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string IpModificacion { get; set; }
    }
}
