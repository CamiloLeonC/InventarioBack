using BackSistemaUbala.Models.Enums;
using Microsoft.OData.ModelBuilder;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackSistemaUbala.Models
{
    public class NotaModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdMateriaProfesor { get; set; }
        public int? IdAlumno { get; set; }
        public string IdUser { get; set; }
        public Periodo Periodo { get; set; }
        /// <summary>
        /// Nota definitiva del saber.
        /// </summary>
        [Column(TypeName = "decimal(4, 2)")]
        public decimal NotaSaber { get; set; }
        /// <summary>
        /// Nota definitiva del hacer.
        /// </summary>
        [Column(TypeName = "decimal(4, 2)")]
        public decimal NotaHacer { get; set; }
        /// <summary>
        /// Nota definitiva del ser.
        /// </summary>
        [Column(TypeName = "decimal(4, 2)")]
        public decimal NotaSer { get; set; }
        /// <summary>
        /// Definitiva total de periodo.
        /// </summary>
        [Column(TypeName = "decimal(4, 2)")]
        public decimal DefinitivaTotal { get; set; }
        public string Observaciones { get; set; }
        public MateriaProfesorModel MateriaProfesor { get; set; }
        public AlumnoModel Alumno { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
    public static class NotaExtension
    {
        public static ModelBuilder NotaMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<NotaModel>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });

            entity.HasOne(x => x.MateriaProfesor)
               .WithMany(y => y.Notas)
               .HasForeignKey(x => x.IdMateriaProfesor)
               .OnDelete(DeleteBehavior.NoAction); // no ON DELETE

            entity.HasOne(x => x.ApplicationUser)
               .WithMany(y => y.NotasAlumnos)
               .HasForeignKey(x => x.IdUser)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.NoAction); // no ON DELETE

            entity.HasOne(x => x.Alumno)
               .WithMany(y => y.NotasAlumnos)
               .HasForeignKey(x => x.IdAlumno)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.NoAction); // no ON DELETE

            return modelBuilder;
        }
        #region EF Mapping

        public static void NotaMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<NotaModel>(nameof(NotaModel));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }

    }
    #endregion
}