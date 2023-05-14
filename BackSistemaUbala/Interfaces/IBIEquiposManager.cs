using BackSistemaUbala.Models;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface IEquiposManager
    {
        IQueryable<EquiposModel> GetAll();
        EquiposModel GetById(int keyEquiposModel);
        EquiposModel Add(EquiposModel row);
        EquiposModel Delete(int keyEquiposModel);
        EquiposModel Update(int keyEquiposModel, EquiposModel changes);
        public byte[] ExportarEquipos();
    }
}
