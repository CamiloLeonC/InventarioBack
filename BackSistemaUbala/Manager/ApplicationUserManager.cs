using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using BackSistemaUbala.Models.DTOs;
using BackSistemaUbala.Models.Enums;
using BackSistemaUbala.Models.InteractionResult;
using General_back.Helpers;
using General_back.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly AplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly ILogger<ApplicationUserManager> logger;
        private readonly SignInManager<ApplicationUser> signInManager;


        public ApplicationUserManager(AplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration config, ILogger<ApplicationUserManager> logger, SignInManager<ApplicationUser> signInManager
)
        {
            this.context = context;
            this.userManager = userManager;
            this.config = config;
            this.logger = logger;
            this.signInManager = signInManager;
        }


        #region Login
        async Task<LoginInteractionResult> IApplicationUserManager.Login(string username, string password)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(username, password, false, true);
                if (result.Succeeded)
                {
                    var ApplicationUser = context.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
                    var exito = new LoginInteractionResult()
                    {
                        Value = $"Bienvenido {ApplicationUser.NombreCompleto}",
                        Success = true,
                        Error = "Inicio de sesión existoso.",
                    };
                    return exito;
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
                var falla = new LoginInteractionResult()
                {
                    Value = "",
                    Success = false,
                    Error = "Error interno en inicio de sesión."
                };
                return falla;
            }
        }

        #endregion

        public IQueryable<ApplicationUser> GetAll()
        {
            return context.ApplicationUsers;
        }

        public UsuarioDTO GetById(string keyApplicationUserId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ApplicationUser result = null;
            UsuarioDTO userResult = null;

            try
            {
                result = context.ApplicationUsers.Where((x) => x.Id == keyApplicationUserId).FirstOrDefault();
                if(result != null)
                {
                    userResult = new UsuarioDTO
                    {
                        Id = result.Id,
                        Documento = result.Documento,
                        Email = result.Email,
                        NombreCompleto = result.NombreCompleto,
                        Celular = result.Celular
                    };

                    var rol = context.ApplicationUserRole.Include(z => z.Role).FirstOrDefault(x => x.UserId == result.Id);
                    if(rol != null)
                    {
                        userResult.IdRol = rol.RoleId;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Usuario no encontrado");
            }

            return userResult;
        }

        async Task<InteractionResult> IApplicationUserManager.Add(UsuarioDTO row)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string userId = "";
            IdentityResult result = null;
            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");

                var user = new ApplicationUser
                {

                    NombreCompleto = row.NombreCompleto,
                    Celular = row.Celular,
                    Documento = row.Documento,
                    Email = row.Email,
                    UserName = row.Email.Trim(),
                };

                //var contrasena = RandomString(7);
                var contrasena = "123456789";

                result = await userManager.CreateAsync(user, contrasena);

                if (result.Succeeded)
                {
                    userId = context.ApplicationUsers.Where(x => x.UserName == row.Email.Trim()).Select(x => x.Id).FirstOrDefault();
                    var roles = row.Roles.Select(x => new ApplicationUserRole() { UserId = userId, RoleId = x });
                    context.ApplicationUserRole.AddRange(roles);
                    context.SaveChanges();
                    string asunto = "Usuario de sistema ubala";
                    string cuerpo = $"Hola, a continuación se entregan sus credenciales para acceder a {config["URLFront"]} \nUsuario: {user.Documento} \nContraseña: {contrasena}";

                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        EnviarCorreoHelper.enviarCorreo(config, asunto, cuerpo, user.Email);
                    }
                    var nuevoUsuario = context.ApplicationUsers.AsNoTracking().
                                        Include(x => x.Roles).FirstOrDefault(x => x.Id == userId);
                    if (nuevoUsuario.Roles != null)
                    {
                        nuevoUsuario.Roles = nuevoUsuario.Roles.ToList().Select(x =>
                        {
                            x.User = null;
                            return x;
                        }
                        ).ToList();
                    }
                }
                else
                {
                    return new InteractionResult()
                    {
                        Error = "Error interno en la creación del usuario.",
                        Success = false
                    };
                }

            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, $"Finalizada operación: {e.Message}");
                return new InteractionResult()
                {
                    Error = "Error interno en la creación del usuario.",
                    Success = false
                };
            }
            finally
            {
                logger.Log(LogLevel.Debug, $"Finalizada operación: {methodName}");
            }
            if (result.Succeeded)
                return new InteractionResult()
                {
                    Value = userId,
                    Error = "Creación de usuario exitosa",
                    Success = true
                };
            else
                return new InteractionResult()
                {
                    Error = result.Errors.FirstOrDefault().Description,
                    Success = false
                };
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public ApplicationUser Delete(string keyApplicationUserId)
        {
            ApplicationUser result = null;
            try
            {
                result = context.ApplicationUsers.FirstOrDefault(x => x.Id == keyApplicationUserId);
                if (result == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                else
                {
                    context.ApplicationUsers.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public ApplicationUser Update(string keyApplicationUserId, UsuarioDTO changes)
        {
            ApplicationUser result = null;
            try
            {
                result = context.ApplicationUsers.FirstOrDefault(x => x.Id == keyApplicationUserId);
                if (result != null)
                {
                    if (existeXDocumento(changes.Id, changes.Documento)) throw new Exception("Ya existe un Usuario con ese Documento.");
                    if (existeXEmail(changes.Id, changes.Email)) throw new Exception("Ya existe un Usuario con ese Correo.");

                    result.NombreCompleto = changes.NombreCompleto;
                    result.Email = changes.Email;
                    result.Documento = changes.Documento;
                    result.Celular = changes.Celular;

                    var rol = context.ApplicationUserRole.FirstOrDefault(x => x.UserId == keyApplicationUserId);
                    if(rol != null)
                    {
                        rol.User = null;
                        rol.Role = null;
                        context.Remove(rol);
                        context.SaveChanges();
                    }    

                    var roles = changes.Roles.Select(x => new ApplicationUserRole() { UserId = keyApplicationUserId, RoleId = x });
                    context.ApplicationUserRole.AddRange(roles);
                    context.SaveChanges();

                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el Usuario");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public byte[] ExportarUsuario()
        {
            throw new NotImplementedException();
        }
        private bool existeXDocumento(string IdUsuario, string documento)
        {
            return context.ApplicationUsers.Any(x => x.Id != IdUsuario && x.Documento == documento);
        }
        private bool existeXEmail(string IdUsuario, string Email)
        {
            return context.ApplicationUsers.Any(x => x.Id != IdUsuario && x.Email == Email);
        }
    }
}
