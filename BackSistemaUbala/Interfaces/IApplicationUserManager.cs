using BackSistemaUbala.Models;
using BackSistemaUbala.Models.DTOs;
using BackSistemaUbala.Models.InteractionResult;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Interfaces
{
    public interface IApplicationUserManager
    {
        IQueryable<ApplicationUser> GetAll();
        Task<LoginInteractionResult> Login(string username, string password);
        UsuarioDTO GetById(string keyApplicationUserId);
        Task<InteractionResult> Add(UsuarioDTO row);
        ApplicationUser Delete(string keyApplicationUserId);
        ApplicationUser Update(string keyApplicationUserId, UsuarioDTO changes);
        public byte[] ExportarUsuario();

    }
}
