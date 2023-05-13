using BackSistemaUbala.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

namespace General_back.Security.Models
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public int ClaimId { get; set; }
        public ApplicationClaim Claim { get; set; }
        public string ClaimPermission { get; set; }

        //[ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }
    }
    #region EF Mapping

    /// <summary>
    /// Extensión para registrar mapping con el Entity Framework y oData
    /// </summary>
    public static class ApplicationRoleClaim_Extension
    {
        /// <summary>
        /// Mapping con el Entity Framework
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static ModelBuilder ApplicationRoleClaimMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ApplicationRoleClaim>();
            entity.HasAlternateKey(c => new
            {
                c.RoleId,
                c.ClaimId
            });

            entity.Property(x => x.ClaimType)
                .HasDefaultValue(string.Empty);

            entity.Property(x => x.ClaimValue)
                .HasDefaultValue(string.Empty);

            entity.HasOne(x => x.Role)
               .WithMany(x => x.Funcionalidades)
               .HasForeignKey(x => x.RoleId)
               .OnDelete(DeleteBehavior.Restrict); // no ON DELETE

            return modelBuilder;
        }
        /// <summary>
        /// Mapping con el oData Framework
        /// </summary>
        /// <param name="oDataModelBuilder"></param>
        public static void ApplicationRoleClaimMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ApplicationRoleClaim>(nameof(ApplicationRoleClaim));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

            ////Ignored properties for oData
            // entityConfig.EntityType.Ignore(x => x.Summary);
        }
    }

    #endregion

}
