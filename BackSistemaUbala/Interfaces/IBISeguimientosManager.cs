using BackSistemaUbala.Models;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface ISeguimientosManager
    {
        IQueryable<SeguimientosModel> GetAll();
        SeguimientosModel GetById(int keySeguimientosModel);
        SeguimientosModel Add(SeguimientosModel row);
        SeguimientosModel Delete(int keySeguimientosModel);
        SeguimientosModel Update(int keySeguimientosModel, SeguimientosModel changes);
        public byte[] ExportarSeguimientos();
    }
}
