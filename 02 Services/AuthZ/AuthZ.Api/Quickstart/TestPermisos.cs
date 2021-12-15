// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AuthZ.Cliente;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;

namespace AuthZ.Api.Quickstart
{
    public class TestPermisos
    {
        public static List<UsuarioPermiso> Permisos
        {
            get
            {                
                return new List<UsuarioPermiso>
                {
                    new UsuarioPermiso
                    {
                        SubjectId = "254d14ac-1843-405d-b470-d75d71343c15",     //jjaramillor
                        Roles = new List<RolAuthZ> {
                            new RolAuthZ
                            {
                                Nombre = "Rol1",
                                Permisos = new List<AuthZ.Cliente.PermisosEnum>
                                {
                                    PermisosEnum.StockAddNew,
                                    PermisosEnum.StockRemove
                                }
                            }
                        }
                    }
                };
            }

        }

    }
}