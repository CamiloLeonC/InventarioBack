using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using BackSistemaUbala.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class EntregaDevolucionesManager : IEntregaDevolucionesManager
    {
        private readonly AplicationDbContext context;

        public EntregaDevolucionesManager(AplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<EntregaDevolucionesModel> GetAll()
        {
            return context.EntregasDevoluciones;
        }

        public EntregaDevolucionesModel GetById(int keyEntregaDevolucionesModel)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            EntregaDevolucionesModel result = null;

            try
            {
                result = context.EntregasDevoluciones.Where((x) => x.Id == keyEntregaDevolucionesModel).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("EntregaDevoluciones no encontrado");
            }

            return result;
        }

        public EntregaDevolucionesModel Add(EntregaDevolucionesModel row)
        {
            EntregaDevolucionesModel result = null;
            try
            {
                result = context.EntregasDevoluciones.FirstOrDefault(x => x.Id == row.Id);
                if (result != null)
                {
                    throw new Exception("Id de EntregaDevoluciones duplicado.");
                }
                else
                {
                    if (existeXCursoGrado(row.Id, row.Codigo)) throw new Exception("Ya existe una entrega o devolución con ese código.");
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

        public EntregaDevolucionesModel Update(int keyEntregaDevolucionesModel, EntregaDevolucionesModel changes)
        {
            EntregaDevolucionesModel result = null;
            try
            {
                result = context.EntregasDevoluciones.FirstOrDefault(x => x.Id == changes.Id);
                if (result != null)
                {
                    result.Codigo = changes.Codigo;
                    result.IdEquipos = changes.IdEquipos;
                    result.IdUsuario = changes.IdUsuario;
                    result.FechaIncio = changes.FechaIncio;
                    result.FechaFin = changes.FechaFin;

                    if (existeXCursoGrado(changes.Id, changes.Codigo)) throw new Exception("Ya existe una entrega o devolución con ese código.");
                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el EntregaDevoluciones");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public EntregaDevolucionesModel Delete(int keyEntregaDevolucionesModel)
        {
            EntregaDevolucionesModel result = null;
            try
            {
                result = context.EntregasDevoluciones.FirstOrDefault(x => x.Id == keyEntregaDevolucionesModel);
                if (result == null)
                {
                    throw new Exception("EntregaDevoluciones no encontrado");
                }
                else
                {
                    context.EntregasDevoluciones.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public byte[] ExportarEntregaDevoluciones()
        {
            throw new NotImplementedException();
        }
        private bool existeXCursoGrado(int id, string cod)
        {
            return context.EntregasDevoluciones.Any(x => x.Id != id && x.Codigo == cod);
        }
    }

}