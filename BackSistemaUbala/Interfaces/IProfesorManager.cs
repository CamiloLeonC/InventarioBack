using BackSistemaUbala.Models;
using System;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface IProfesorManager
    {
        IQueryable<ProfesorModel> GetAll();
        ProfesorModel GetById(int keyProfesorModelId);
        ProfesorModel Add(ProfesorModel row);
        ProfesorModel Delete(int keyProfesorModelId);
        ProfesorModel Update(int keyProfesorModelId, ProfesorModel changes);
        public byte[] ExportarProfesor();
    }
}
