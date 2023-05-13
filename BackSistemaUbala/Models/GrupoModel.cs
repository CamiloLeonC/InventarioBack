using BackSistemaUbala.Models.Enums;
using Microsoft.OData.ModelBuilder;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackSistemaUbala.Models
{
    public class GrupoModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Curso Curso { get; set; }
        public Grado Grado { get; set; }
        public ICollection<ProfesorModel> Profesores { get; set; }
        public ICollection<AlumnoModel> Alumnos { get; set; }
        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
    public static class GrupoExtension
    {
        public static ModelBuilder GrupoMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<GrupoModel>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });
            return modelBuilder;
        }
        #region EF Mapping

        public static void GrupoMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<GrupoModel>(nameof(GrupoModel));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }

    }
    #endregion
}
