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
    [Authorize(Roles = "Administrador,Docente, Alumno")]

    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoManager AlumnoManager;

        public AlumnoController(IAlumnoManager AlumnoManager)
        {
            this.AlumnoManager = AlumnoManager;
        }
        // GET: api/<AlumnoController>
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                var listAlumno = AlumnoManager.GetAll();
                return Ok(listAlumno);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<AlumnoController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                var idAlumno = AlumnoManager.GetById(id);
                return Ok(idAlumno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        
        }

        // POST api/<AlumnoController>
        [HttpPost]
        public IActionResult Post([FromBody] AlumnoModel alumno)
        {
            try
            {
                var result = AlumnoManager.Add(alumno);
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

        // PUT api/<AlumnoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AlumnoModel alumno)
        {
            try
            {
                var result = AlumnoManager.Update(id,alumno);
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

        // DELETE api/<AlumnoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = AlumnoManager.Delete(id);
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
