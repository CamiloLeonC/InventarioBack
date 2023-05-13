using BackSistemaUbala.Models;
using System;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface IApplicationRoleManager
    {
        IQueryable<ApplicationRole> GetAll();
        ApplicationRole RolForUser(string keyUser);
        ApplicationRole GetById(string keyUser);
        ApplicationRole Add(ApplicationRole row);
        ApplicationRole Delete(string keyRolModelId);
        ApplicationRole Update(string keyRolModelId, ApplicationRole changes);
        public byte[] ExportarRol();
    }
}
