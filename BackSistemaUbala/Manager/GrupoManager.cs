using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using BackSistemaUbala.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class GrupoManager : IGrupoManager
    {
        private readonly AplicationDbContext context;

        public GrupoManager(AplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<GrupoModel> GetAll()
        {
            return context.Grupos;
        }

        public GrupoModel GetById(int keyGrupoModelId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            GrupoModel  result = null;

            try
            {
                result = context.Grupos.Where((x) => x.Id == keyGrupoModelId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Grupo no encontrado");
            }

            return result;
        }

        public GrupoModel Add(GrupoModel row)
        {
            GrupoModel result = null;
            try
            {
                result = context.Grupos.FirstOrDefault(x => x.Id == row.Id);
                if (result != null)
                {
                    throw new Exception("Id de grupo duplicado.");
                }
                else
                {
                    if (existeXCursoGrado(row.Id, row.Curso, row.Grado)) throw new Exception("Ya existe un curso asignado con ese grado.");
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

        public GrupoModel Delete(int keyGrupoModelId)
        {
            GrupoModel result = null;
            try
            {
                result = context.Grupos.FirstOrDefault(x => x.Id == keyGrupoModelId);
                if (result == null)
                {
                    throw new Exception("Grupo no encontrado");
                }
                else
                {
                    context.Grupos.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public GrupoModel Update(int keyGrupoModelId, GrupoModel changes)
        {
            GrupoModel result = null;
            try
            {
                result = context.Grupos.FirstOrDefault(x => x.Id == changes.Id);
                if (result != null)
                {
                    result.Curso = changes.Curso;
                    result.Grado = changes.Grado;

                    if (existeXCursoGrado(changes.Id, changes.Curso, changes.Grado)) throw new Exception("Ya existe un curso asignado con ese grado.");
                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el grupo");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public byte[] ExportarGrupo()
        {
            throw new NotImplementedException();
        }
        private bool existeXCursoGrado(int IdGrupo, Curso curso, Grado grado)
        {
            return context.Grupos.Any(x => x.Id != IdGrupo && x.Curso == curso && x.Grado == grado);
        }
    }
    
}
