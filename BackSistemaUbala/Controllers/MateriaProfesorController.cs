using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System;

namespace BackSistemaUbala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaProfesorController : ControllerBase
    {
        private readonly IMateriaProfesorManager MateriaProfesorManager;

        public MateriaProfesorController(IMateriaProfesorManager MateriaProfesorManager)
        {
            this.MateriaProfesorManager = MateriaProfesorManager;
        }
        // GET: api/<MateriaProfesorController>
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult Get()
        {
            try
            {
                var listMateriaProfesor = MateriaProfesorManager.GetAll();
                return Ok(listMateriaProfesor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<MateriaProfesorController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult GetById(int id)
        {
            try
            {
                var idMateriaProfesor = MateriaProfesorManager.GetById(id);
                return Ok(idMateriaProfesor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<MateriaProfesorController>
        [HttpPost]
        [Authorize(Roles = "Administrador")]

        public IActionResult Post([FromBody] MateriaProfesorModel alumno)
        {
            try
            {
                var result = MateriaProfesorManager.Add(alumno);
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

        // PUT api/<MateriaProfesorController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Put(int id, [FromBody] MateriaProfesorModel alumno)
        {
            try
            {
                var result = MateriaProfesorManager.Update(id, alumno);
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

        // DELETE api/<MateriaProfesorController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Delete(int id)
        {
            try
            {
                var result = MateriaProfesorManager.Delete(id);
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
