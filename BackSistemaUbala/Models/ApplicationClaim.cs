using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace General_back.Security.Models
{
    public class ApplicationClaim
    {
        [Key]
        public int IdClaim { get; set; }
        public int ClaimCode { get; set; }
        public string ClaimName { get; set; }
        public string ClaimPestanas { get; set; }
        public string ClaimDescription { get; set; }
        public ICollection<ApplicationRoleClaim> RolesClaims { get; set; }
        public ICollection<ApplicationUserClaim> UsersClaims { get; set; }
        public int ModuloId { get; set; }

    }
    #region EF Mapping

    /// <summary>
    /// Extensión para registrar mapping con el Entity Framework y oData
    /// </summary>
    public static class ApplicationClaim_Extension
    {
        /// <summary>
        /// Mapping con el Entity Framework
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static ModelBuilder ApplicationClaimMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ApplicationClaim>();
            // PrimaryKey
            entity.HasKey(c => new { c.IdClaim });
            entity.ToTable("AspNetClaim");
            //entity.HasOne(typeof(SimModulos), "SimModulos")
            //    .WithMany()
            //    .HasForeignKey("ModuloId")
            //    .OnDelete(DeleteBehavior.Restrict); // no ON DELETE

            return modelBuilder;
        }
        /// <summary>
        /// Mapping con el oData Framework
        /// </summary>
        /// <param name="oDataModelBuilder"></param>
        public static void ApplicationClaimMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ApplicationClaim>(nameof(ApplicationClaim));

            var entity = entityConfig.EntityType;
            // PrimaryKey
            entity.HasKey(c => new { c.IdClaim });

            ////Ignored properties for oData
            // entityConfig.EntityType.Ignore(x => x.Summary);
        }
    }    
    #endregion
}
