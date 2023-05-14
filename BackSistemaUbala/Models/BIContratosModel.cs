using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackSistemaUbala.Models
{
    public class ContratosModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdEquipo { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaIncio { get; set; }
        public DateTime FechaFin { get; set; }        
        public string TerminosContrato { get; set; }
        public EquiposModel Equipo { get; set; }
    }

    public static class ContratosExtension
    {
        #region EF Mapping
        public static ModelBuilder ContratosMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ContratosModel>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });

            entity.HasOne(x => x.Equipo)
               .WithMany(x => x.Contratos)
               .HasForeignKey(x => x.IdEquipo)
               .OnDelete(DeleteBehavior.Restrict); // no ON DELETE

            return modelBuilder;
        }

        public static void ContratosMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ContratosModel>(nameof(ContratosModel));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }
        #endregion
    }
}
