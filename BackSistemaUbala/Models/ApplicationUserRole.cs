using BackSistemaUbala.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

namespace General_back.Security.Models
{
    public class ApplicationUserRole:IdentityUserRole<string>
    {
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
    public static class ApplicationUserRole_Extension
    {

        public static ModelBuilder ApplicationUserRoleMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ApplicationUserRole>();
            // PrimaryKey
            entity.HasKey(c => new { c.UserId, c.RoleId });

            modelBuilder.Entity<ApplicationUserRole>()
               .HasOne(u => u.User)
               .WithMany(u => u.Roles)
               .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<ApplicationUserRole>()
               .HasOne(u => u.Role)
               .WithMany(u => u.Users)
               .HasForeignKey(bc => bc.RoleId);

            return modelBuilder;
        }
        /// <summary>
        /// Mapping con el oData Framework
        /// </summary>
        /// <param name="oDataModelBuilder"></param>
        public static void ApplicationUserRoleMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ApplicationUserRole>(nameof(ApplicationUserRole));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.UserId,c.RoleId });


            ////Ignored properties for oData
            // entityConfig.EntityType.Ignore(x => x.Summary);
        }
    }
}
