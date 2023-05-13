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

            modelBuilder.AlumnoMapping();
            modelBuilder.GrupoMapping();
            modelBuilder.MateriaMapping();
            modelBuilder.MateriaProfesorMapping();
            modelBuilder.NotaMapping();
            modelBuilder.ProfesorMapping();


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


            #region Ubala
            services.AddScoped<IAlumnoManager, AlumnoManager>();
            services.AddScoped<IGrupoManager, GrupoManager>();
            services.AddScoped<IMateriaManager, MateriaManager>();
            services.AddScoped<IMateriaProfesorManager, MateriaProfesorManager>();
            services.AddScoped<INotaManager, NotaManager>();
            services.AddScoped<IProfesorManager, ProfesorManager>();
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
            services.AddScoped<ISeguridadManager, SeguridadManager>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();


            #endregion


        }
    } 
}

