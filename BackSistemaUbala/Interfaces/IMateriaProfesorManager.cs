using BackSistemaUbala.Models;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface IMateriaProfesorManager
    {
        IQueryable<MateriaProfesorModel> GetAll();
        MateriaProfesorModel GetById(int keyMateriaProfesorModelId);
        MateriaProfesorModel Add(MateriaProfesorModel row);
        MateriaProfesorModel Delete(int keyMateriaProfesorModelId);
        MateriaProfesorModel Update(int keyMateriaProfesorModelId, MateriaProfesorModel changes);
        public byte[] ExportarMateriaProfesor();
    }
}
