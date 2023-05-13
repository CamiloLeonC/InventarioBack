using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

namespace General_back.Security.Models
{
    public class ApplicationUserLogin: IdentityUserLogin<string>
    {
    }
    public static class ApplicationUserLogin_Extension
    {
        /// <summary>
        /// Mapping con el oData Framework
        /// </summary>
        /// <param name="oDataModelBuilder"></param>
        /// 
        public static ModelBuilder ApplicationUserLoginMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ApplicationUserLogin>();

            //PrimaryKey
            entity.HasNoKey();
      

            return modelBuilder;
        }
        public static void ApplicationUserLoginMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ApplicationUserLogin>(nameof(ApplicationUserLogin));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.LoginProvider, c.ProviderKey });

            ////Ignored properties for oData
            // entityConfig.EntityType.Ignore(x => x.Summary);
        }
    }
}
