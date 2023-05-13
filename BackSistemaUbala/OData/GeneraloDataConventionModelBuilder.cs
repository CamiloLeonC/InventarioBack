using BackSistemaUbala.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.ModelBuilder;

namespace General_back.oData
{
    public class GeneraloDataConventionModelBuilder : ODataConventionModelBuilder
    {
        ConventionModelBuilder _general;

        public GeneraloDataConventionModelBuilder(IServiceCollection provider) : base()
        {
            //oData Entity Mapping
            _general = new ConventionModelBuilder(this);
        }

    }

    public static class extensions 
    {
        public static void AddODataScoped(this IServiceCollection services) 
        {
            ConventionModelBuilder.AddODataScoped(services);
        }

    } 
}
