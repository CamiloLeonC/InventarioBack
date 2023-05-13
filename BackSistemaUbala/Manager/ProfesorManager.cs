using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class ProfesorManager : IProfesorManager
    {
        private readonly AplicationDbContext context;

        public ProfesorManager(AplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ProfesorModel> GetAll()
        {
            return context.Profesores;
        }

        public ProfesorModel GetById(int keyProfesorModelId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ProfesorModel result = null;

            try
            {
                result = context.Profesores.Where((x) => x.Id == keyProfesorModelId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Profesor no encontrado");
            }

            return result;
        }

        public ProfesorModel Add(ProfesorModel row)
        {
            ProfesorModel result = null;
            try
            {
                result = context.Profesores.FirstOrDefault(x => x.Documento == row.Documento);
                if (result != null)
                {
                    throw new Exception("Documento de profesor duplicado.");
                }
                else
                {
                    if (existeXDocumento(row.Id, row.Documento)) throw new Exception("Ya existe un Profesor con ese Documento.");
                    if (existeXCorreo(row.Id, row.Correo)) throw new Exception("Ya existe un Profesor con ese Correo.");
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

        public ProfesorModel Delete(int keyProfesorModelId)
        {
            ProfesorModel result = null;
            try
            {
                result = context.Profesores.FirstOrDefault(x => x.Id == keyProfesorModelId);
                if (result == null)
                {
                    throw new Exception("Profesor no encontrado");
                }
                else
                {
                    context.Profesores.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public ProfesorModel Update(int keyProfesorModelId, ProfesorModel changes)
        {
            ProfesorModel result = null;
            try
            {
                result = context.Profesores.FirstOrDefault(x => x.Id == changes.Id);
                if (result != null)
                {
                    result.IdGrupo = changes.IdGrupo;
                    result.NombreCompleto = changes.NombreCompleto;
                    result.Correo = changes.Correo;
                    result.Documento = changes.Documento;
                    if (existeXDocumento(changes.Id, changes.Documento)) throw new Exception("Ya existe un Profesor con ese Documento.");
                    if (existeXCorreo(changes.Id, changes.Correo)) throw new Exception("Ya existe un Profesor con ese Correo.");
                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el profesor");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public byte[] ExportarProfesor()
        {
            throw new NotImplementedException();
        }

        private bool existeXDocumento(int IdAlumno, string documento)
        {
            return context.Profesores.Any(x => x.Id != IdAlumno && x.Documento == documento);
        }
        private bool existeXCorreo(int IdAlumno, string correo)
        {
            return context.Profesores.Any(x => x.Id != IdAlumno && x.Correo == correo);
        }

    }
}
