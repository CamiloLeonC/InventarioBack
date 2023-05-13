//SIM_backBOG_Semestre.cs

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace General_back.Security.Models
{
    public class ApplicationUserTokenHelper
    {
        public static string GetJTIFromAuthorization(HttpRequest request)
        {
            return GetTokenInfoFromAuthorization(request, "jti");
        }
        public static string GetRolesFromAuthorization(HttpRequest request)
        {
            return GetTokenInfoFromAuthorization(request, "rol");
        }

        public static string GetEmailFromAuthorization(HttpRequest request)
        {
            return GetTokenInfoFromAuthorization(request, "mail");
        }

        public static string GetIdFromAuthorization(HttpRequest request)
        {
            return GetTokenInfoFromAuthorization(request, "id");
        }

        public static string GetNameFromAuthorization(HttpRequest request)
        {
            return GetTokenInfoFromAuthorization(request, "given_name");
        }

        private static string GetTokenInfoFromAuthorization(HttpRequest request, string tokenInfo)
        {
            //Obtenemos el token de autorizacion
            request.Headers.TryGetValue("Authorization", out StringValues authorization);

            if (authorization.Count == 0)
                throw new Exception("No se encontraron los datos de autenticación.");

            //extraemos información del usuario
            var jwt = authorization[0].Replace("Bearer", "").Trim();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            //Obtenemos el correo del usuario
            return token.Claims.FirstOrDefault(x => x.Type == tokenInfo)?.Value;
        }
    }
}
