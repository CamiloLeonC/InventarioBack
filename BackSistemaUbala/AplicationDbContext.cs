using BackSistemaUbala.Models;
using General_back.Security.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackSistemaUbala
{
    //DbContext crea una instacia a nuestra base de datos, lo cual permite almacenar los datos, hacer querys, crear base de datos apartir de nuestro modelo.
    public class AplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>


    {
        #region Inventario
        public DbSet<ContratosModel> Contratos { get; set; }
        public DbSet<EntregaDevolucionesModel> EntregasDevoluciones { get; set; }
        public DbSet<EquiposModel> Equipos { get; set; }
        public DbSet<SeguimientosModel> Seguimientos { get; set; }
        #endregion

        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
        public virtual DbSet<ApplicationUserLogin> ApplicationUserLogin { get; set; }
        public virtual DbSet<ApplicationClaim> ApplicationClaim { get; set; }
        public virtual DbSet<ApplicationRoleClaim> ApplicationRoleClaim { get; set; }
        public virtual DbSet<ApplicationUserClaim> ApplicationUserClaim { get; set; }
        public virtual DbSet<ApplicationUserToken> ApplicationUserToken { get; set; }




        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.EquiposMapping();
            modelbuilder.ContratosMapping();
            modelbuilder.EntregaDevolucionesMapping();
            modelbuilder.SeguimientosMapping();

            modelbuilder.ApplicationUserMapping();
            modelbuilder.ApplicationRoleMapping();
            modelbuilder.ApplicationUserRoleMapping();
            modelbuilder.ApplicationClaimMapping();
            modelbuilder.ApplicationUserClaimMapping();
            modelbuilder.ApplicationRoleClaimMapping();



        }
    }
}
