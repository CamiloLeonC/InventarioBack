using Microsoft.AspNetCore.Identity;
using Microsoft.OData.ModelBuilder;

namespace General_back.Security.Models
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
    }
    public static class ApplicationUserToken_Extension
    {
        /// <summary>
        /// Mapping con el oData Framework
        /// </summary>
        /// <param name="oDataModelBuilder"></param>
        public static void ApplicationUserTokenMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ApplicationUserToken>(nameof(ApplicationUserToken));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.UserId, c.LoginProvider, c.Name });

            ////Ignored properties for oData
            // entityConfig.EntityType.Ignore(x => x.Summary);
        }
    }
}
