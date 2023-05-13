using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BackSistemaUbala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaManager MateriaManager;

        public MateriaController(IMateriaManager MateriaManager)
        {
            this.MateriaManager = MateriaManager;
        }
        // GET: api/<MateriaController>
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult Get()
        {
            try
            {
                var listMateria = MateriaManager.GetAll();
                return Ok(listMateria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<MateriaController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult GetById(int id)
        {
            try
            {
                var idMateria = MateriaManager.GetById(id);
                return Ok(idMateria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        // POST api/<MateriaController>
        [HttpPost]
        [Authorize(Roles = "Administrador")]

        public IActionResult Post([FromBody] MateriaModel alumno)
        {
            try
            {
                var result = MateriaManager.Add(alumno);
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

        // PUT api/<MateriaController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Put(int id, [FromBody] MateriaModel alumno)
        {
            try
            {
                var result = MateriaManager.Update(id, alumno);
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

        // DELETE api/<MateriaController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Delete(int id)
        {
            try
            {
                var result = MateriaManager.Delete(id);
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
