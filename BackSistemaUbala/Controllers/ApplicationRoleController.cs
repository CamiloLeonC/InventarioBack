using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;


namespace BackSistemaUbala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationRoleController : ControllerBase
    {
        private readonly IApplicationRoleManager ApplicationRoleManager;

        public ApplicationRoleController(IApplicationRoleManager ApplicationRoleManager)
        {
            this.ApplicationRoleManager = ApplicationRoleManager;
        }
        // GET: api/<RolController>
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult Get()
        {
            try
            {
                var listRol = ApplicationRoleManager.GetAll();
                return Ok(listRol);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<RolController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult GetById(string id)
        {

            try
            {
                var idRol = ApplicationRoleManager.GetById(id);
                return Ok(idRol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        
        }

        [HttpGet("/api/RolForUser/{id}")]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult RolForUser(string id)
        {
            try
            {
                var idRol = ApplicationRoleManager.RolForUser(id);
                return Ok(idRol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // POST api/<RolController>
        [HttpPost]
        [Authorize(Roles = "Administrador")]

        public IActionResult Post([FromBody] ApplicationRole Rol)
        {
            try
            {
                var result = ApplicationRoleManager.Add(Rol);
                if(result == null)
                {
                    return BadRequest("Error en el sistema, contacte al administrador.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<RolController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Put(string id, [FromBody] ApplicationRole Rol)
        {
            try
            {
                var result = ApplicationRoleManager.Update(id,Rol);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Error en el sistema, contacte al administrador.");
                }
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<RolController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Delete(string id)
        {
            try
            {
                var result = ApplicationRoleManager.Delete(id);
                if (result == null)
                {
                    return NotFound();
                }              
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
