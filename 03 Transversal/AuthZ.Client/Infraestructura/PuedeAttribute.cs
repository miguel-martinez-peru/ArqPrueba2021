using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthZ.Cliente
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class PuedeAttribute : AuthorizeAttribute
    {
        public PuedeAttribute(PermisosEnum permission) : base(((int)permission).ToString())
        { }
    }
}
