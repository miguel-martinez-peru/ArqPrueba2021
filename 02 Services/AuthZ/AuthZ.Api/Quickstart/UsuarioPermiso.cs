using AuthZ.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.Api.Quickstart
{
    public class UsuarioPermiso
    {
        public string SubjectId { get; set; }
        public List<RolAuthZ> Roles { get; set; }
    }

    public class RolAuthZ
    {
        public string Nombre { get; set; }
        public List<PermisosEnum> Permisos { get; set; }
    }
}
