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

    public class EntregasDevolucionesController : ControllerBase
    {
        private readonly IEntregaDevolucionesManager EntregasDevolucionesManager;

        public EntregasDevolucionesController(IEntregaDevolucionesManager EntregasDevolucionesManager)
        {
            this.EntregasDevolucionesManager = EntregasDevolucionesManager;
        }
        // GET: api/<EntregasDevolucionesController>
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Administrador,Docente, Alumno")]
        public IActionResult Get()
        {
            try
            {
                var listEntregasDevoluciones = EntregasDevolucionesManager.GetAll();
                return Ok(listEntregasDevoluciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<EntregasDevolucionesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult GetById(int id)
        {
            try
            {
                var idEntregasDevoluciones = EntregasDevolucionesManager.GetById(id);
                return Ok(idEntregasDevoluciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<EntregasDevolucionesController>
        [HttpPost]
        [Authorize(Roles = "Administrador")]

        public IActionResult Post([FromBody] EntregaDevolucionesModel alumno)
        {
            try
            {
                    var result = EntregasDevolucionesManager.Add(alumno);
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

        // PUT api/<EntregasDevolucionesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Put(int id, [FromBody] EntregaDevolucionesModel alumno)
        {
            try
            {
                var result = EntregasDevolucionesManager.Update(id, alumno);
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

        // DELETE api/<EntregasDevolucionesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Delete(int id)
        {
            try
            {
                var result = EntregasDevolucionesManager.Delete(id);
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
