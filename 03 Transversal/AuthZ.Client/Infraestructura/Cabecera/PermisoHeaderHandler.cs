using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AuthZ.Cliente.Cabecera
{

    public class PermisoHeaderHandler : AuthorizationHandler<PermisoRequirement>
    {
        IHttpContextAccessor _httpContextAccessor = null;
        private readonly IHttpClientFactory _httpClientFactory;

        public PermisoHeaderHandler(IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermisoRequirement requirement)
        {
            //obtengo el SUB del token (falta validar el token)
            //var bearer = AuthenticationHttpContextExtensions.GetTokenAsync(_httpContextAccessor.HttpContext, "access_token");
            //var handler = new JwtSecurityTokenHandler();
            //var token = handler.ReadJwtToken(bearer.Result);

            //obtengo el header GUID_ROL
            var existe = _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("GUID_ROL");
            if(!existe)
                return;

            //MODO 1 - LLAMADA A AUTHZ API
            //aca tenemos la libertad de validar el permiso del usuario a la funcionalidad
            //no se carga el bearer con los permisos ya que no es responsabilidad del identity server, se valida contra el authorization server
            var request = new { 
                sub = _httpContextAccessor.HttpContext.Request.Headers["GUID_ROL"].ToString(),
                permiso = int.Parse(requirement.PermissionName)
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //ServicePointManager.ServerCertificateValidationCallback =
            //    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            //    {
            //        return true;
            //    };


            var apiClient = _httpClientFactory.CreateClient("AuthZ");

            var url = $"AuthZ/Valida";
            var reqst = new HttpRequestMessage(HttpMethod.Post, url);
            reqst.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            using (var response = await apiClient.SendAsync(reqst))
            {
                if (response.IsSuccessStatusCode)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }


            //var response = await apiClient.PostAsync("/AuthZ/Valida", content);
            //if (response.IsSuccessStatusCode)
            //{
            //    context.Succeed(requirement);
            //}
            //else
            //{
            //    context.Fail();
            //}

            return;
        }
    }
}
