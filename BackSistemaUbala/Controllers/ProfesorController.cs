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
    [Authorize(Roles = "Docente")]

    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorManager ProfesorManager;

        public ProfesorController(IProfesorManager ProfesorManager)
        {
            this.ProfesorManager = ProfesorManager;
        }
        // GET: api/<ProfesorController>
        [HttpGet]
        [EnableQuery]

        public IActionResult Get()
        {
            try
            {
                var listProfesor = ProfesorManager.GetAll();
                return Ok(listProfesor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProfesorController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var idProfesor = ProfesorManager.GetById(id);
                return Ok(idProfesor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ProfesorController>
        [HttpPost]
        public IActionResult Post([FromBody] ProfesorModel Profesor)
        {
            try
            {
                var result = ProfesorManager.Add(Profesor);
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

        // PUT api/<ProfesorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProfesorModel Profesor)
        {
            try
            {
                var result = ProfesorManager.Update(id, Profesor);
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

        // DELETE api/<ProfesorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = ProfesorManager.Delete(id);
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
