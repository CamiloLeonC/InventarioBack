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

    public class SeguimientosController : ControllerBase
    {
        private readonly ISeguimientosManager SeguimientosManager;

        public SeguimientosController(ISeguimientosManager SeguimientosManager)
        {
            this.SeguimientosManager = SeguimientosManager;
        }
        // GET: api/<SeguimientosController>
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Administrador,Docente, Alumno")]
        public IActionResult Get()
        {
            try
            {
                var listSeguimientos = SeguimientosManager.GetAll();
                return Ok(listSeguimientos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<SeguimientosController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult GetById(int id)
        {
            try
            {
                var idSeguimientos = SeguimientosManager.GetById(id);
                return Ok(idSeguimientos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<SeguimientosController>
        [HttpPost]
        [Authorize(Roles = "Administrador")]

        public IActionResult Post([FromBody] SeguimientosModel alumno)
        {
            try
            {
                    var result = SeguimientosManager.Add(alumno);
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

        // PUT api/<SeguimientosController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Put(int id, [FromBody] SeguimientosModel alumno)
        {
            try
            {
                var result = SeguimientosManager.Update(id, alumno);
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

        // DELETE api/<SeguimientosController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Delete(int id)
        {
            try
            {
                var result = SeguimientosManager.Delete(id);
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
