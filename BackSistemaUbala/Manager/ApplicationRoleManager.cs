using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BackSistemaUbala.Manager
{
    public class ApplicationRoleManager : IApplicationRoleManager
    {
        private readonly AplicationDbContext context;
        private object logger;
        private readonly RoleManager<ApplicationRole> roleManager;


        public ApplicationRoleManager(AplicationDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            this.context = context;
            this.roleManager = roleManager;

        }

        public IQueryable<ApplicationRole> GetAll()
        {
            return context.ApplicationRole;
        }

        public ApplicationRole GetById(string keyRolModelId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ApplicationRole result = null;

            try
            {
                result = context.ApplicationRole.Where((x) => x.Id == keyRolModelId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("Rol no encontrado");
            }
           
            return result;
        }

        public ApplicationRole RolForUser(string keyUser)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ApplicationRole result = null;

            try
            {
                var rol = context.ApplicationUserRole.Include(z => z.Role).FirstOrDefault(x => x.UserId == keyUser);

                if(rol != null)
                {
                    result = rol.Role;
                    result.Users = null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Rol no encontrado");
            }

            return result;
        }

        public ApplicationRole Add(ApplicationRole row)
        {
            ApplicationRole result = null;

            try
            {
                row.Id = Guid.NewGuid().ToString();
                var roleresult = roleManager.CreateAsync(row).Result;
                if (roleresult.Succeeded) //Role insertado
                {
                    //Guardamos todos los cambios
                    try
                    {
                        result = row;
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    throw new Exception("Error creando el : " +
                                        string.Join(", ", roleresult.Errors));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
            return result;
        }

        public ApplicationRole Delete(string keyRolModelId)
        {
            ApplicationRole result = null;
            try
            {
                result = context.ApplicationRole.FirstOrDefault(x => x.Id == keyRolModelId);
                if (result == null)
                {
                    throw new Exception("Rol no encontrado");
                }
                else
                {
                    context.ApplicationRole.Remove(result);
                    context.SaveChanges();
                }      
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public ApplicationRole Update(string keyRolModelId, ApplicationRole changes)
        {
            ApplicationRole result = null;
            try
            {
                result = context.ApplicationRole.FirstOrDefault(x => x.Id == keyRolModelId);
                if (result != null)               
                {
                    result.Name = changes.Name;
                    result.Descripcion = changes.Descripcion;

                    if (existeXName(changes.Id, changes.Name)) throw new Exception("Ya existe un Rol con ese Nombre.");
                    if (existeXDescripcíon(changes.Id, changes.Descripcion)) throw new Exception("Ya existe un Rol con esa Descripción.");
                    if (existeXCombinacion(changes.Id, changes.Name, changes.Descripcion)) throw new Exception("Ya existe un Rol con esa combinación de Nombre y descripción.");

                    context.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró el Rol");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        
        public byte[] ExportarRol()
        {
            throw new NotImplementedException();
        }
        private bool existeXName(string IdRol, string Name)
        {
            return context.ApplicationRole.Any(x => x.Id != IdRol && x.Name == Name);
        }
        private bool existeXDescripcíon(string IdRol, string descripcion)
        {
            return context.ApplicationRole.Any(x => x.Id != IdRol && x.Descripcion == descripcion);
        }
        private bool existeXCombinacion(string IdRol, string Name,string descripcion)
        {
            return context.ApplicationRole.Any(x => x.Id != IdRol && x.Name == Name && x.Descripcion == descripcion);
        }
    }
}
