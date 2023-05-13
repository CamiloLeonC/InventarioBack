using BackSistemaUbala.Models;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface IMateriaManager
    {
        IQueryable<MateriaModel> GetAll();
        MateriaModel GetById(int keyMateriaModelId);
        MateriaModel Add(MateriaModel row);
        MateriaModel Delete(int keyMateriaModelId);
        MateriaModel Update(int keyMateriaModelId, MateriaModel changes);
        public byte[] ExportarMateria();
    }
}
