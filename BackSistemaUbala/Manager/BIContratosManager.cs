using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using BackSistemaUbala.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Manager
{
    public class ContratosManager : IContratosManager
    {
        private readonly AplicationDbContext context;

        public ContratosManager(AplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ContratosModel> GetAll()
        {
            return context.Contratos;
        }

        public ContratosModel GetById(int keyContratosModel)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ContratosModel result = null;

            try
            {
                result = context.Contratos.Where((x) => x.Id == keyContratosModel).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Contratos no encontrado");
            }

            return result;
        }

        public ContratosModel Add(ContratosModel row)
        {
            ContratosModel result = null;
            try
            {
                result = context.Contratos.FirstOrDefault(x => x.Id == row.Id);
                if (result != null)
                {
                    throw new Exception("Id de Contratos duplicado.");
                }
                else
                {
                    if (existeXCursoGrado(row.Id, row.Codigo)) throw new Exception("Ya existe un contrato con ese código.");
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

        public ContratosModel Update(int keyContratosModel, ContratosModel changes)
        {
            ContratosModel result = null;
            try
            {
                result = context.Contratos.FirstOrDefault(x => x.Id == changes.Id);
                if (result != null)
                {
                    result.Codigo = changes.Codigo;
                    result.IdEquipo = changes.IdEquipo;
                    result.FechaIncio = changes.FechaIncio;
                    result.FechaFin = changes.FechaFin;
                    result.TerminosContrato = changes.TerminosContrato;

                    if (existeXCursoGrado(changes.Id, changes.Codigo)) throw new Exception("Ya existe un contrato con ese código.");
                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el Contratos");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public ContratosModel Delete(int keyContratosModel)
        {
            ContratosModel result = null;
            try
            {
                result = context.Contratos.FirstOrDefault(x => x.Id == keyContratosModel);
                if (result == null)
                {
                    throw new Exception("Contratos no encontrado");
                }
                else
                {
                    context.Contratos.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public byte[] ExportarContratos()
        {
            throw new NotImplementedException();
        }
        private bool existeXCursoGrado(int id, string cod)
        {
            return context.Contratos.Any(x => x.Id != id && x.Codigo == cod);
        }
    }

}