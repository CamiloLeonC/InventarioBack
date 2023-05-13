using BackSistemaUbala.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackSistemaUbala.Models
{
    public class AlumnoModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public string NombreCompleto { get; set; }
        public Jornada Jornada { get; set; }
        public TipoSangre TipoSangre { get; set; }
        public string Correo { get; set; }
        public string Documento { get; set; }
        public string NombreAcudiente { get; set; }
        public string NumeroAcudiente { get; set; }
        public GrupoModel Grupo { get; set; }
        public ICollection<NotaModel> NotasAlumnos { get; set; }
    }

    public static class AlumnoExtension
    {
        public static ModelBuilder AlumnoMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<AlumnoModel>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });

            entity.HasOne(x => x.Grupo)
               .WithMany(x => x.Alumnos)
               .HasForeignKey(x => x.IdGrupo)
               .OnDelete(DeleteBehavior.Restrict); // no ON DELETE

            return modelBuilder;
        }
        #region EF Mapping

        public static void AlumnoMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<AlumnoModel>(nameof(AlumnoModel));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }

    }
    #endregion
}
