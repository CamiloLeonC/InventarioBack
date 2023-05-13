using Microsoft.OData.ModelBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Models
{
    public class ProfesorModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public string NombreCompleto { get; set; }
        public string Documento { get; set; }
        public string Correo { get; set; }
        public GrupoModel Grupo { get; set; }
        public ICollection<MateriaProfesorModel> MateriasProfesor { get; set; }


    }
    public static class ProfesorExtension
    {
        public static ModelBuilder ProfesorMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ProfesorModel>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });

            entity.HasOne(x => x.Grupo)
               .WithMany(x => x.Profesores)
               .HasForeignKey(x => x.IdGrupo)
               .OnDelete(DeleteBehavior.NoAction); // no ON DELETE

            return modelBuilder;
        }
        #region EF Mapping

        public static void ProfesorMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ProfesorModel>(nameof(ProfesorModel));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }

    }
    #endregion
}
