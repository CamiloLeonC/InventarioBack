using BackSistemaUbala.Models;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface IEntregaDevolucionesManager
    {
        IQueryable<EntregaDevolucionesModel> GetAll();
        EntregaDevolucionesModel GetById(int keyEntregaDevolucionesModel);
        EntregaDevolucionesModel Add(EntregaDevolucionesModel row);
        EntregaDevolucionesModel Delete(int keyEntregaDevolucionesModel);
        EntregaDevolucionesModel Update(int keyEntregaDevolucionesModel, EntregaDevolucionesModel changes);
    }
}
