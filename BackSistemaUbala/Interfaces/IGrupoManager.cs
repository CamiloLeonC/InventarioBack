using BackSistemaUbala.Models;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface IGrupoManager
    {
        IQueryable<GrupoModel> GetAll();
        GrupoModel GetById(int keyGrupoModelId);
        GrupoModel Add(GrupoModel row);
        GrupoModel Delete(int keyGrupoModelId);
        GrupoModel Update(int keyGrupoModelId, GrupoModel changes);
        public byte[] ExportarGrupo();
    }
}
