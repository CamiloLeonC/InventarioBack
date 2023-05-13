using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using General_back.Security.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Http;
using General_back.Helpers;
using Microsoft.EntityFrameworkCore;
using General_back.Enums.Models;
using BackSistemaUbala.Models;
using BackSistemaUbala;
using BackSistemaUbala.Models.InteractionResult;

namespace General_back.Security.Managers
{
    public class SeguridadManager : ISeguridadManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly AplicationDbContext context;
        private readonly ILogger<ISeguridadManager> logger;
        private readonly IConfiguration config;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SignInManager<ApplicationUser> signInManager;

        public SeguridadManager(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            AplicationDbContext context,
            ILogger<SeguridadManager> logger,
            IConfiguration config,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.logger = logger;
            this.config = config;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }
       

        #region Login
        async Task<LoginInteractionResult> ISeguridadManager.Login(string username, string password)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(username, password, false, true);
                if (result.Succeeded)
                {
                    ApplicationUser user = context.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
                    
                    var token = await GenerateJwtToken(user);
                    if (token != null)
                    {
                        var exito = new LoginInteractionResult()
                        {
                            Value = await GenerateJwtToken(user),
                            Success = true,
                            Error = "Inicio de sesión existoso.",
                        };
                                                
                        return exito;
                    }
                    else
                    {
                        return new LoginInteractionResult()
                        {
                            Value = "",
                            Success = false,
                            Error = "Error creación de token."
                        };
                    }
                }
                else
                {
                    var resp = new LoginInteractionResult()
                    {
                        Value = "",
                        Success = false,
                        Error = "Error en inicio de sesión, usuario o contraseña incorrecta. "
                    };

                    return resp;
                }
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, $"Error en inicio de sesión: {e.Message}");
                var falla =  new LoginInteractionResult()
                {
                    Value = "",
                    Success = false,
                    Error = "Error interno en inicio de sesión."
                };
                                
                return falla;
            }
        }

        public InteractionResult ValidateToken(string token)
        {
            InteractionResult resp = new InteractionResult()
            {
                Success = false,
                Error = "Operación fallida, tóken no válido "
            };
            try
            {
                SecurityToken secToken = GetPrincipal(token);
                if (secToken != null)
                {
                    resp.Success = true;
                    resp.Error = "Operación exitosa";
                    return resp;
                }
                else
                    return resp;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, $"Error en ValidateToken: \n{ex.Message}\n{ex.StackTrace}");
                return resp;
            }

        }

        private SecurityToken GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                IdentityModelEventSource.ShowPII = true;
                if (jwtToken == null)
                    return null;
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = config["URLubala"],
                    ValidAudience = "https://localhost:5002",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7AUW3u4mF3QOmdEBr4mQIlxcjT4h6E")),//Configuration["JwtKey"]
                    ClockSkew = TimeSpan.Zero // remove delay of token when expire
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return securityToken;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, $"Error en validando token en GetPrincipal: \n{ex.Message}\n{ex.StackTrace}");
                return null;
            }
        }

        public async Task<IdentityResult> ChangePassword(string actualPassword, string newPassword, string user)
        {
            ApplicationUser usr = context.ApplicationUsers.Where(x => x.Id == user).FirstOrDefault();
            IdentityResult result = await userManager.ChangePasswordAsync(usr, actualPassword, newPassword);
            
            return result;
        }

        public async Task<InteractionResult> ActualizarRecuperarClave(string cedula, string token, string pass)
        {
            var resp = new InteractionResult
            {
                Success = false,
                Error = "Operación fallida"
            };
            try
            {
                ApplicationUser usr = context.ApplicationUsers.Where(x => x.UserName == cedula).FirstOrDefault();
                IdentityResult result = await userManager.ResetPasswordAsync(usr, token, pass);
                if (!result.Succeeded)
                    return new InteractionResult
                    {
                        Success = false,
                        Error = "Token no válido"
                    };
                else
                    resp = new InteractionResult
                    {
                        Success = true,
                        Error = "Contraseña reestablecida correctamente."
                    };
                return resp;
            }
            catch (Exception)
            {
                resp = new InteractionResult
                {
                    Success = false,
                    Error = "Error en la operación"
                };
                return resp;
            }
        }

        public async Task<InteractionResult> RecuperarClave(string cedula)
        {
            ApplicationUser usr = context.ApplicationUsers.FirstOrDefault(x => x.UserName == cedula);

            var result = new InteractionResult
            {
                Success = false,
                Error = "Error recuperando contraseña."
            };

            if (usr != null)
            {
                try
                {
                    string token = await userManager.GeneratePasswordResetTokenAsync(usr);
                    token = HttpUtility.UrlEncode(token);

                    string asunto = "Recuperación clave usuario Sistema de Ubala.";
                    string cuerpo = $"Hola, a continuación se encuentra un link para restaurar su contraseña: {config["URLFront"] + "/RecuperarClave?id=" + token}&cedula={cedula}";

                    EnviarCorreoHelper.enviarCorreo(config, asunto, cuerpo, usr.NormalizedEmail);

                    result = new InteractionResult
                    {
                        Success = true,
                        Error = "Se envió correo de recuperación."
                    };
                }
                catch (Exception e)
                {
                    result = new InteractionResult
                    {
                        Success = false,
                        Error = $"Error recuperando contraseña. {e.Message}"
                    };
                    
                }
            }

            return result;
        }

        public InteractionResult CambiarContraseniaAdmin(string idUser, string pass)
        {
            ApplicationUser usr = context.ApplicationUsers.FirstOrDefault(x => x.Id == idUser);

            var result = new InteractionResult
            {
                Success = false,
                Error = "Error cambiando contraseña."
            };

            if (usr == null)
            {
                result.Error = "No se ha encontrado el usuario con el identificador especificado.";
                return result;
            }
            else
            {
                try
                {
                    string token = userManager.GeneratePasswordResetTokenAsync(usr).Result;
                    IdentityResult identityResult = userManager.ResetPasswordAsync(usr, token, pass).Result;
                    if (identityResult.Succeeded)
                    {
                        result.Success = true;
                        result.Error = "";
                        result.Value = "Cambio exitoso.";
                    }
                }
                catch (Exception e)
                {
                    logger.Log(LogLevel.Error, $"Error en CambiarContraseniaAdmin: \n{e.Message}\n{e.StackTrace}");
                    result.Error = $"Error cambiando contraseña.";
                }
            }

            return result;
        }

        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            //TODO Agregar superAdmin al token
            JwtSecurityToken token = null;
            try
            {
                string rol = context.ApplicationUserRole.Include(y => y.Role).FirstOrDefault(x => x.UserId == user.Id).Role.Descripcion;
                var claims = new List<System.Security.Claims.Claim>
                {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id),
                    new System.Security.Claims.Claim("given_name", user.NombreCompleto),
                    new System.Security.Claims.Claim("id", user.Id),
                    new System.Security.Claims.Claim("rol", rol),
                    new Claim(ClaimTypes.Role, rol),
                    new System.Security.Claims.Claim("mail", string.IsNullOrEmpty(user.NormalizedEmail) ? "" : user.NormalizedEmail),
                    new System.Security.Claims.Claim("username", user.UserName),
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7AUW3u4mF3QOmdEBr4mQIlxcjT4h6E"));//config["JwtKey"]
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(1.0); //config["JwtExpireDays"]

                token = new JwtSecurityToken(
                    config["URLubala"],//TODO
                    config["URLubala"],//TODO
                    claims,
                    expires: expires,
                    signingCredentials: creds
                );
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, $"Error generando token: {e.Message}");
                throw;
            }


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       

        #endregion
    }

}
