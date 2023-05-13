using Microsoft.OData.ModelBuilder;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackSistemaUbala.Models
{
    public partial class MateriaProfesorModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? IdProfesor { get; set; }
        public string IdUser { get; set; }
        public int IdMateria { get; set; }
        public ProfesorModel Profesor { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public MateriaModel Materia { get; set; }
        public ICollection<NotaModel> Notas { get; set; }
    }

    public static class MateriaProfesorExtension
    {
        public static ModelBuilder MateriaProfesorMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<MateriaProfesorModel>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });

            entity.HasOne(x => x.Profesor)
               .WithMany(x => x.MateriasProfesor)
               .HasForeignKey(x => x.IdProfesor)              
               .IsRequired(false)
               .OnDelete(DeleteBehavior.NoAction); // no ON DELETE

            entity.HasOne(x => x.ApplicationUser)
               .WithMany(x => x.MateriasProfesor)
               .HasForeignKey(x => x.IdUser)
               .OnDelete(DeleteBehavior.NoAction); // no ON DELETE

            entity.HasOne(x => x.Materia)
              .WithMany(x => x.MateriaProfesores)
              .HasForeignKey(x => x.IdMateria)
              .OnDelete(DeleteBehavior.NoAction); // no ON DELETE

            return modelBuilder;
        }
        #region EF Mapping

        public static void MateriaProfesorMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<MateriaProfesorModel>(nameof(MateriaProfesorModel));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }

    }
    #endregion
}
