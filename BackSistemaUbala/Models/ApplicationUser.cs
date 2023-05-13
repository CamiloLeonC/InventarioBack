using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using BackSistemaUbala.Models.Enums;
using System.Collections.Generic;
using General_back.Security.Models;

namespace BackSistemaUbala.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? IdGrupo { get; set; }
        public string NombreCompleto { get; set; }
        public Jornada Jornada { get; set; }
        public TipoSangre TipoSangre { get; set; }
        public string Documento { get; set; }
        public string NombreAcudiente { get; set; }
        public string NumeroAcudiente { get; set; }
        public GrupoModel Grupo { get; set; }
        public ICollection<NotaModel> NotasAlumnos { get; set; }
        public ICollection<MateriaProfesorModel> MateriasProfesor { get; set; }
        public ICollection<ApplicationUserRole> Roles { get; set; }

    }
    public static class ApplicationUser_Extension
    {
        public static ModelBuilder ApplicationUserMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ApplicationUser>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });

            entity.HasOne(x => x.Grupo)
               .WithMany(x => x.ApplicationUser)
               .HasForeignKey(x => x.IdGrupo)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Restrict);

            //HasOne Profesor dee ir aca?


            return modelBuilder;
        }

        #region EF Mapping

        public static void ApplicationUserMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ApplicationUser>(nameof(ApplicationUser));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }

    }
    #endregion
}
