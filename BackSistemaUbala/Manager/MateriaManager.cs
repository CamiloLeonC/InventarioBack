using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class MateriaManager : IMateriaManager
    {
        private readonly AplicationDbContext context;

        public MateriaManager(AplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<MateriaModel> GetAll()
        {
            return context.Materias;
        }

        public MateriaModel GetById(int keyMateriaModelId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            MateriaModel result = null;

            try
            {
                result = context.Materias.Where((x) => x.Id == keyMateriaModelId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Materia no encontrada");
            }

            return result;
        }

        public MateriaModel Add(MateriaModel row)
        {
            MateriaModel result = null;
            try
            {
                result = context.Materias.FirstOrDefault(x => x.Id == row.Id);
                if (result != null)
                {
                    throw new Exception("Id de la materia duplicado.");
                }
                else
                {
                    if (existeXCod(row.Id, row.Cod)) throw new Exception("Ya existe una materia con ese código.");
                    if (existeXCodNom(row.Id, row.Nombre, row.Cod)) throw new Exception("Ya existe una materia con esa combinacion de código y nombre.");
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

        public MateriaModel Delete(int keyMateriaModelId)
        {
            MateriaModel result = null;
            try
            {
                result = context.Materias.FirstOrDefault(x => x.Id == keyMateriaModelId);
                if (result == null)
                {
                    throw new Exception("Materia no encontrada");
                }
                else
                {
                    context.Materias.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public MateriaModel Update(int keyMateriaModelId, MateriaModel changes)
        {
            MateriaModel result = null;
            try
            {
                result = context.Materias.FirstOrDefault(x => x.Id == changes.Id);
                if (result != null)
                {
                    result.Cod = changes.Cod;
                    result.Nombre = changes.Nombre;
                    result.Descripcion = changes.Descripcion;
                    if (existeXCod(changes.Id, changes.Cod)) throw new Exception("Ya existe una materia con ese código.");
                    if (existeXCodNom(changes.Id, changes.Nombre, changes.Cod)) throw new Exception("Ya existe una materia con esa combinacion de código y nombre.");
                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró la materia");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public byte[] ExportarMateria()
        {
            throw new NotImplementedException();
        }
        private bool existeXCod(int idMateria, string codigo)
        {
            return context.Materias.Any(x => x.Id != idMateria && x.Cod == codigo);
        }
        private bool existeXCodNom(int idMateria, string nombre, string codigo)
        {
            return context.Materias.Any(x => x.Id != idMateria && x.Nombre == nombre && x.Cod == codigo);
        }
    }
}
