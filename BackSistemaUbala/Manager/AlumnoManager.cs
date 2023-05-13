using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class AlumnoManager : IAlumnoManager
    {
        private readonly AplicationDbContext context;
        private object logger;

        public AlumnoManager(AplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<AlumnoModel> GetAll()
        {
            return context.Alumnos;
        }

        public AlumnoModel GetById(int keyAlumnoModelId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            AlumnoModel result = null;

            try
            {
                result = context.Alumnos.Where((x) => x.Id == keyAlumnoModelId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Alumno no encontrado");
            }
           
            return result;
        }

        public AlumnoModel Add(AlumnoModel row)
        {
            AlumnoModel result = null;
            try
            {
                result = context.Alumnos.FirstOrDefault(x => x.Documento == row.Documento);
                if(result != null)
                {
                    throw new Exception("Documento de alumno duplicado.");
                }
                else
                {
                    if (existeXDocumento(row.Id, row.Documento)) throw new Exception("Ya existe un Alumno con ese Documento.");
                    if (existeXCorreo(row.Id, row.Correo)) throw new Exception("Ya existe un Alumno con ese Correo.");
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

        public AlumnoModel Delete(int keyAlumnoModelId)
        {
            AlumnoModel result = null;
            try
            {
                result = context.Alumnos.FirstOrDefault(x => x.Id == keyAlumnoModelId);
                if (result == null)
                {
                    throw new Exception("Alumno no encontrado");
                }
                else
                {
                    context.Alumnos.Remove(result);
                    context.SaveChanges();
                }      
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public AlumnoModel Update(int keyAlumnoModelId, AlumnoModel changes)
        {
            AlumnoModel result = null;
            try
            {
                result = context.Alumnos.FirstOrDefault(x => x.Id == changes.Id);
                if (result != null)               
                {
                    result.IdGrupo = changes.IdGrupo;
                    result.NombreCompleto = changes.NombreCompleto;
                    result.Jornada = changes.Jornada;
                    result.TipoSangre = changes.TipoSangre;
                    result.Correo = changes.Correo;
                    result.Documento = changes.Documento;
                    result.NombreAcudiente = changes.NombreAcudiente;
                    result.NumeroAcudiente = changes.NumeroAcudiente;

                    if (existeXDocumento(changes.Id, changes.Documento)) throw new Exception("Ya existe un Alumno con ese Documento.");
                    if (existeXCorreo(changes.Id, changes.Correo)) throw new Exception("Ya existe un Alumno con ese Correo.");

                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el alumno");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        
        public byte[] ExportarAlumno()
        {
            throw new NotImplementedException();
        }
        private bool existeXDocumento(int IdAlumno, string documento)
        {
            return context.Alumnos.Any(x => x.Id != IdAlumno && x.Documento == documento);
        }
        private bool existeXCorreo(int IdAlumno, string correo)
        {
            return context.Alumnos.Any(x => x.Id != IdAlumno && x.Correo == correo);
        } 
    }
}
