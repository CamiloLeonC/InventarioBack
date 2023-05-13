using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

namespace General_back.Security.Models
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public bool? Denied { get; set; }
        public int ClaimId { get; set; }
        public ApplicationClaim Claim { get; set; }
    }
    #region EF Mapping

    /// <summary>
    /// Extensión para registrar mapping con el Entity Framework y oData
    /// </summary>
    public static class ApplicationUserClaim_Extension
    {
        /// <summary>
        /// Mapping con el Entity Framework
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static ModelBuilder ApplicationUserClaimMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ApplicationUserClaim>();
            // Llave unicas
            entity.HasAlternateKey(c => new { c.UserId, c.ClaimId });

            //Valores por defecto
            entity.Property(x => x.ClaimType)
                .HasDefaultValue(string.Empty);
            entity.Property(x => x.ClaimValue)
                .HasDefaultValue(string.Empty);

            return modelBuilder;
        }
        /// <summary>
        /// Mapping con el oData Framework
        /// </summary>
        /// <param name="oDataModelBuilder"></param>
        public static void ApplicationUserClaimMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ApplicationUserClaim>(nameof(ApplicationUserClaim));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

            ////Ignored properties for oData
            // entityConfig.EntityType.Ignore(x => x.Summary);
        }
    }

    #endregion
}
