using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AuthZ.Api.Application.Commands
{
    public class AuditoriaCommand
    {
        [DataMember]
        public Guid UsuarioCreacion { get; set; }
        [DataMember]
        public DateTime FechaCreacion { get; set; }
        [DataMember]
        public string IpCreacion { get; set; }
        [DataMember]
        public Guid? UsuarioModificacion { get; set; }
        [DataMember]
        public DateTime? FechaModificacion { get; set; }
        [DataMember]
        public string IpModificacion { get; set; }
    }
}
