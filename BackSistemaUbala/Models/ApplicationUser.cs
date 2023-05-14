using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BackSistemaUbala.Models.Enums;
using System.Collections.Generic;
using General_back.Security.Models;

namespace BackSistemaUbala.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NombreCompleto { get; set; }
        public string Documento { get; set; }
        public string Celular { get; set; }    
        public ICollection<ApplicationUserRole> Roles { get; set; }
        public ICollection<EntregaDevolucionesModel> EntregaDevoluciones { get; set; }
        

    }
    public static class ApplicationUser_Extension
    {
        public static ModelBuilder ApplicationUserMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ApplicationUser>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });
            
            return modelBuilder;
        }

        #region EF Mapping

        public static void ApplicationUserMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ApplicationUser>(nameof(ApplicationUser));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }

    }
    #endregion
}
