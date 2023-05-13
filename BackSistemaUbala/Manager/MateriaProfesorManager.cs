using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class MateriaProfesorManager : IMateriaProfesorManager
    {
        private readonly AplicationDbContext context;

        public MateriaProfesorManager(AplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<MateriaProfesorModel> GetAll()
        {
            return context.MateriaProfesores;
        }

        public MateriaProfesorModel GetById(int keyMateriaProfesorModelId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            MateriaProfesorModel result = null;

            try
            {
                result = context.MateriaProfesores.Where((x) => x.Id == keyMateriaProfesorModelId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Materia por profesor no encontrada");
            }

            return result;
        }

        public MateriaProfesorModel Add(MateriaProfesorModel row)
        {
            MateriaProfesorModel result = null;
            try
            {
                result = context.MateriaProfesores.FirstOrDefault(x => x.Id == row.Id);
                if (result != null)
                {
                    throw new Exception("Id de la materia por profesor duplicado.");
                }
                else
                {
                    if (existeProfesorMateria(row.Id, row.IdUser, row.IdMateria)) throw new Exception("La materia ya esta asignada al profesor.");
                    result = row;
                    context.Add(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public MateriaProfesorModel Delete(int keyMateriaProfesorModelId)
        {
            MateriaProfesorModel result = null;
            try
            {
                result = context.MateriaProfesores.FirstOrDefault(x => x.Id == keyMateriaProfesorModelId);
                if (result == null)
                {
                    throw new Exception("Materia por profesor no encontrado");
                }
                else
                {
                    context.MateriaProfesores.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public MateriaProfesorModel Update(int keyMateriaProfesorModelId, MateriaProfesorModel changes)
        {
            MateriaProfesorModel result = null;
            try
            {
                result = context.MateriaProfesores.FirstOrDefault(x => x.Id == changes.Id);
                if (result != null)
                {
                    result.IdMateria = changes.IdMateria;
                    result.IdUser = changes.IdUser;

                    if (existeProfesorMateria(changes.Id, changes.IdUser, changes.IdMateria)) throw new Exception("La materia ya esta asignada al profesor.");

                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el materia por profesor");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public byte[] ExportarMateriaProfesor()
        {
            throw new NotImplementedException();
        }

        private bool existeProfesorMateria(int Id, string IdUser, int idMateria)
        {
            return context.MateriaProfesores.Any(x => x.Id != Id && x.IdUser == IdUser && x.IdMateria == idMateria);
        }
    }
}
