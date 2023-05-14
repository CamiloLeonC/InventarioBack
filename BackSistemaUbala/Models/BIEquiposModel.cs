using BackSistemaUbala.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackSistemaUbala.Models
{
    public class EquiposModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string TipoEquipo { get; set; }
        public string NumeroSerie { get; set; }
        public string Placa { get; set; }
        public string MarcaEquipo { get; set; }        
        public string ModeloEquipo { get; set; }
        public string RamEquipo { get; set; }
        public string DDEquipo { get; set; }
        public string SOEquipo { get; set; }
        public ICollection<EntregaDevolucionesModel> EntregaDevoluciones { get; set; }
        public ICollection<ContratosModel> Contratos { get; set; }

    }

    public static class EquiposExtension
    {
        #region EF Mapping
        public static ModelBuilder EquiposMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EquiposModel>();

            //PrimaryKey
            entity.HasKey(c => new { c.Id });
                        

            return modelBuilder;
        }

        public static void EquiposMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<EquiposModel>(nameof(EquiposModel));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.Id });

        }
        #endregion
    }
}
