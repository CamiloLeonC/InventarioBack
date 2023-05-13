using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using BackSistemaUbala.Models.InteractionResult;
using System.Security.Claims;

namespace General_back.Security.Interfaces
{
    public interface ISeguridadManager
    {
        public Task<IdentityResult> ChangePassword(string actualPassword, string newPassword, string user);
        public Task<InteractionResult> ActualizarRecuperarClave(string correo, string token, string pass);
        public Task<InteractionResult> RecuperarClave(string correo);
        public InteractionResult ValidateToken(string token);
        Task<LoginInteractionResult> Login(string username, string password);
        public InteractionResult CambiarContraseniaAdmin(string userid, string pass);


    }
}
