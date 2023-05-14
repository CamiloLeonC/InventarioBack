using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Manager;
using BackSistemaUbala.Models;
using General_back.Security.Interfaces;
using General_back.Security.Managers;
using General_back.Security.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.ModelBuilder;

namespace BackSistemaUbala.OData
{
    public class ConventionModelBuilder
    {
        public ConventionModelBuilder(ODataConventionModelBuilder modelBuilder) 
        {

            #region Ubala

            modelBuilder.EquiposMapping();
            modelBuilder.EntregaDevolucionesMapping();
            modelBuilder.ContratosMapping();
            modelBuilder.SeguimientosMapping();

            modelBuilder.ApplicationUserRoleMapping();
            modelBuilder.ApplicationUserMapping();
            modelBuilder.ApplicationRoleMapping();
            modelBuilder.ApplicationClaimMapping();
            modelBuilder.ApplicationRoleClaimMapping();
            modelBuilder.ApplicationUserClaimMapping();
            modelBuilder.ApplicationUserLoginMapping();
            modelBuilder.ApplicationUserTokenMapping();


            #endregion


        }

        public static void AddODataScoped(IServiceCollection services) 
        {
            services.AddScoped<IEntregaDevolucionesManager, EntregaDevolucionesManager>();
            services.AddScoped<IEquiposManager, EquiposManager>();
            services.AddScoped<IContratosManager, ContratosManager>();
            services.AddScoped<ISeguimientosManager, SeguimientosManager>();

            #region Inventario
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
            services.AddScoped<ISeguridadManager, SeguridadManager>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();


            #endregion


        }
    } 
}

