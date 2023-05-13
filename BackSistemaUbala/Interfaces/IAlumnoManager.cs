using BackSistemaUbala.Models;
using System;
using System.Linq;

namespace BackSistemaUbala.Interfaces
{
    public interface IAlumnoManager
    {
        IQueryable<AlumnoModel> GetAll();
        AlumnoModel GetById(int keyAlumnoModelId);
        AlumnoModel Add(AlumnoModel row);
        AlumnoModel Delete(int keyAlumnoModelId);
        AlumnoModel Update(int keyAlumnoModelId, AlumnoModel changes);
        public byte[] ExportarAlumno();
    }
}
