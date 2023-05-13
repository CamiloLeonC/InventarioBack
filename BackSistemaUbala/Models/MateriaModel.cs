using Microsoft.OData.ModelBuilder;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackSistemaUbala.Models
{
    public class MateriaModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Cod { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ICollection<MateriaProfesorModel> MateriaProfesores { get; set; }
    }
    public static class MateriaExtension
    {
        public static ModelBuilder MateriaMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<MateriaModel>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });

            return modelBuilder;
        }
        #region EF Mapping

        public static void MateriaMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<MateriaModel>(nameof(MateriaModel));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }

    }
    #endregion
}
