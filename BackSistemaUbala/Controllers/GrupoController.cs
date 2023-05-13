using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Data.SqlClient;
using System;

namespace BackSistemaUbala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrador, Docente")]
    //[Authorize(Roles = "Docente")]

    public class GrupoController : ControllerBase
    {
        private readonly IGrupoManager GrupoManager;

        public GrupoController(IGrupoManager GrupoManager)
        {
            this.GrupoManager = GrupoManager;
        }
        // GET: api/<GrupoController>
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Administrador,Docente, Alumno")]
        public IActionResult Get()
        {
            try
            {
                var listGrupo = GrupoManager.GetAll();
                return Ok(listGrupo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<GrupoController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult GetById(int id)
        {
            try
            {
                var idGrupo = GrupoManager.GetById(id);
                return Ok(idGrupo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<GrupoController>
        [HttpPost]
        [Authorize(Roles = "Administrador")]

        public IActionResult Post([FromBody] GrupoModel alumno)
        {
            try
            {
                    var result = GrupoManager.Add(alumno);
                if (result == null)
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

        // PUT api/<GrupoController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Put(int id, [FromBody] GrupoModel alumno)
        {
            try
            {
                var result = GrupoManager.Update(id, alumno);
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

        // DELETE api/<GrupoController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Delete(int id)
        {
            try
            {
                var result = GrupoManager.Delete(id);
                if (result == null)
                {
                    return BadRequest("No se encontro registro, vuelva a intentar.");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("El registro se encuentra en uso.");
            }
        }
    }
}
