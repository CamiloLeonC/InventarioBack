using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using BackSistemaUbala.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class EquiposManager : IEquiposManager
    {
        private readonly AplicationDbContext context;

        public EquiposManager(AplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<EquiposModel> GetAll()
        {
            return context.Equipos;
        }

        public EquiposModel GetById(int keyEquiposModel)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            EquiposModel result = null;

            try
            {
                result = context.Equipos.Where((x) => x.Id == keyEquiposModel).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Equipos no encontrado");
            }

            return result;
        }

        public EquiposModel Add(EquiposModel row)
        {
            EquiposModel result = null;
            try
            {
                result = context.Equipos.FirstOrDefault(x => x.Id == row.Id);
                if (result != null)
                {
                    throw new Exception("Id de Equipos duplicado.");
                }
                else
                {
                    if (existeXCursoGrado(row.Id, row.Codigo)) throw new Exception("Ya existe un equipo con ese código.");
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

        public EquiposModel Update(int keyEquiposModel, EquiposModel changes)
        {
            EquiposModel result = null;
            try
            {
                result = context.Equipos.FirstOrDefault(x => x.Id == changes.Id);
                if (result != null)
                {
                    result.Codigo = changes.Codigo;
                    result.TipoEquipo = changes.TipoEquipo;
                    result.NumeroSerie = changes.NumeroSerie;
                    result.Placa = changes.Placa;
                    result.MarcaEquipo = changes.MarcaEquipo;
                    result.ModeloEquipo = changes.ModeloEquipo;
                    result.RamEquipo = changes.RamEquipo;
                    result.DDEquipo = changes.DDEquipo;
                    result.SOEquipo = changes.SOEquipo;

                    if (existeXCursoGrado(changes.Id, changes.Codigo)) throw new Exception("Ya existe un equipo con ese código.");
                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el Equipos");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public EquiposModel Delete(int keyEquiposModel)
        {
            EquiposModel result = null;
            try
            {
                result = context.Equipos.FirstOrDefault(x => x.Id == keyEquiposModel);
                if (result == null)
                {
                    throw new Exception("Equipos no encontrado");
                }
                else
                {
                    context.Equipos.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public byte[] ExportarEquipos()
        {
            throw new NotImplementedException();
        }
        private bool existeXCursoGrado(int id, string cod)
        {
            return context.Equipos.Any(x => x.Id != id && x.Codigo == cod);
        }
    }

}