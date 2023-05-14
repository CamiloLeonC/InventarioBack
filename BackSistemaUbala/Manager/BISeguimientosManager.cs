using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using BackSistemaUbala.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class SeguimientosManager : ISeguimientosManager
    {
        private readonly AplicationDbContext context;

        public SeguimientosManager(AplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<SeguimientosModel> GetAll()
        {
            return context.Seguimientos;
        }

        public SeguimientosModel GetById(int keySeguimientosModel)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            SeguimientosModel result = null;

            try
            {
                result = context.Seguimientos.Where((x) => x.Id == keySeguimientosModel).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Seguimientos no encontrado");
            }

            return result;
        }

        public SeguimientosModel Add(SeguimientosModel row)
        {
            SeguimientosModel result = null;
            try
            {
                result = context.Seguimientos.FirstOrDefault(x => x.Id == row.Id);
                if (result != null)
                {
                    throw new Exception("Id de Seguimientos duplicado.");
                }
                else
                {
                    if (existeXCursoGrado(row.Id, row.Codigo)) throw new Exception("Ya existe un Seguimiento con ese código.");
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

        public SeguimientosModel Update(int keySeguimientosModel, SeguimientosModel changes)
        {
            SeguimientosModel result = null;
            try
            {
                result = context.Seguimientos.FirstOrDefault(x => x.Id == changes.Id);
                if (result != null)
                {
                    result.Codigo = changes.Codigo;
                    result.IdEntregaDevolucion = changes.IdEntregaDevolucion;
                    result.Estado = changes.Estado;
                    result.FechaEstado = changes.FechaEstado;

                    if (existeXCursoGrado(changes.Id, changes.Codigo)) throw new Exception("Ya existe un Seguimiento con ese código.");
                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el Seguimientos");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public SeguimientosModel Delete(int keySeguimientosModel)
        {
            SeguimientosModel result = null;
            try
            {
                result = context.Seguimientos.FirstOrDefault(x => x.Id == keySeguimientosModel);
                if (result == null)
                {
                    throw new Exception("Seguimientos no encontrado");
                }
                else
                {
                    context.Seguimientos.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public byte[] ExportarSeguimientos()
        {
            throw new NotImplementedException();
        }
        private bool existeXCursoGrado(int id, string cod)
        {
            return context.Seguimientos.Any(x => x.Id != id && x.Codigo == cod);
        }
    }

}