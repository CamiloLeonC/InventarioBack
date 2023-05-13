using Microsoft.OData.ModelBuilder;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using General_back.Security.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackSistemaUbala.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Descripcion { get; set; }
        public ICollection<ApplicationUserRole> Users { get; set; }
        public ICollection<ApplicationRoleClaim> Funcionalidades { get; set; }
    }
    public static class ApplicationRoleExtension
    {
        public static ModelBuilder ApplicationRoleMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ApplicationRole>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });


            return modelBuilder;
        }
        #region EF Mapping

        public static void ApplicationRoleMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ApplicationRole>(nameof(ApplicationRole));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }

        public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
        {
            public void Configure(EntityTypeBuilder<ApplicationRole> builder)
            {
                builder.Metadata.RemoveIndex(new[] { builder.Property(r => r.NormalizedName).Metadata });

                builder.HasIndex(r => new { r.NormalizedName}).HasName("RoleNameIndex").IsUnique();
            }
        }

        public class MyRoleValidator : RoleValidator<ApplicationRole>
        {
            public override async Task<IdentityResult> ValidateAsync(RoleManager<ApplicationRole> manager, ApplicationRole role)
            {
                var roleName = await manager.GetRoleNameAsync(role);
                if (string.IsNullOrWhiteSpace(roleName))
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "RoleNameIsNotValid",
                        Description = "Role Name is not valid!"
                    });
                }
                else
                {
                    var owner = await manager.Roles.FirstOrDefaultAsync(x => x.Id != role.Id && x.NormalizedName.ToLower().Trim() == roleName.ToLower().Trim());

                    if (owner != null && !string.Equals(manager.GetRoleIdAsync(owner), manager.GetRoleIdAsync(role)))
                    {
                        return IdentityResult.Failed(new IdentityError
                        {
                            Code = "DuplicateRoleName",
                            Description = "this role already exist in this App!"
                        });
                    }
                }
                return IdentityResult.Success;
            }
        }

    }
    #endregion
}
