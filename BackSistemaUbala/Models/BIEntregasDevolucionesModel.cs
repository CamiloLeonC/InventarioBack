using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackSistemaUbala.Models
{
    public class EntregaDevolucionesModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string IdUsuario { get; set; }
        public int IdEquipos { get; set; }
        public DateTime FechaIncio { get; set; }
        public DateTime FechaFin { get; set; }
        public ApplicationUser Usuario { get; set; }
        public EquiposModel Equipo { get; set; }

        public ICollection<SeguimientosModel> Seguimientos { get; set; }

    }

    public static class EntregaDevolucionesExtension
    {
        #region EF Mapping
        public static ModelBuilder EntregaDevolucionesMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EntregaDevolucionesModel>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });

            entity.HasOne(x => x.Usuario)
               .WithMany(x => x.EntregaDevoluciones)
               .HasForeignKey(x => x.IdUsuario)
               .OnDelete(DeleteBehavior.Restrict); // no ON DELETE

            entity.HasOne(x => x.Equipo)
               .WithMany(x => x.EntregaDevoluciones)
               .HasForeignKey(x => x.IdEquipos)
               .OnDelete(DeleteBehavior.Restrict); // no ON DELETE

            return modelBuilder;
        }

        public static void EntregaDevolucionesMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<EntregaDevolucionesModel>(nameof(EntregaDevolucionesModel));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }
        #endregion
    }
}
