using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuthZ.Cliente
{

    public class PermissionHandler : AuthorizationHandler<PermisoRequirement>
    {
        IHttpContextAccessor _httpContextAccessor = null;
        private readonly IHttpClientFactory _httpClientFactory;

        public PermissionHandler(IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermisoRequirement requirement)
        {
            //obtengo el SUB del token (falta validar el token)
            var bearer = AuthenticationHttpContextExtensions.GetTokenAsync(_httpContextAccessor.HttpContext, "access_token");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(bearer.Result);
                       

            //MODO 1 - LLAMADA A AUTHZ API
            //aca tenemos la libertad de validar el permiso del usuario a la funcionalidad
            //no se carga el bearer con los permisos ya que no es responsabilidad del identity server, se valida contra el authorization server
            var request = new { 
                sub = token.Claims.FirstOrDefault(x => x.Type == "guid_rol").Value,
                permiso = requirement.PermissionName
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var apiClient = _httpClientFactory.CreateClient("AuthZ");
            var response = await apiClient.PostAsync("AuthZ/Valida", content);
            if (response.IsSuccessStatusCode)
            {
                context.Succeed(requirement);
            }

            return;
        }
    }
}
