using BackSistemaUbala.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackSistemaUbala.Models
{
    public class SeguimientosModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdEntregaDevolucion { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEstado { get; set; }
        public EntregaDevolucionesModel EntregaDevolucion { get; set; }
    }

    public static class SeguimientosExtension
    {
        #region EF Mapping
        public static ModelBuilder SeguimientosMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<SeguimientosModel>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });

            entity.HasOne(x => x.EntregaDevolucion)
               .WithMany(x => x.Seguimientos)
               .HasForeignKey(x => x.IdEntregaDevolucion)
               .OnDelete(DeleteBehavior.Restrict); // no ON DELETE

            return modelBuilder;
        }

        public static void SeguimientosMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<SeguimientosModel>(nameof(SeguimientosModel));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }
        #endregion
    }
}
