using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.Api.Model
{
    public class ValidaRequest
    {
        public Guid sub { get; set; }
        public int permiso { get; set; }
    }
}
