using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using General_back.Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackSistemaUbala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private readonly INotaManager NotaManager;
        private readonly string userId;
        private readonly string userRol;

        public NotaController(INotaManager NotaManager,
                                    IHttpContextAccessor httpContextAccessor)
        {
            this.NotaManager = NotaManager;
            this.userId = ApplicationUserTokenHelper.GetIdFromAuthorization(httpContextAccessor.HttpContext.Request);
            this.userRol = ApplicationUserTokenHelper.GetRolesFromAuthorization(httpContextAccessor.HttpContext.Request);
        }
        // GET: api/<NotaController>
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IEnumerable<NotaModel> Get()
        {
            return NotaManager.GetAll(this.userId, this.userRol);
        }

        // GET api/<NotaController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Docente, Alumno")]
        public IActionResult GetById(int id)
        {
            try
            {
                var idNota = NotaManager.GetById(id);
                return Ok(idNota);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<NotaController>
        [HttpPost]
        [Authorize(Roles = "Administrador,Docente")]

        public IActionResult Post([FromBody] NotaModel alumno)
        {
            try
            {
                var result = NotaManager.Add(alumno);
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

        // PUT api/<NotaController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Docente")]

        public IActionResult Put(int id, [FromBody] NotaModel nota)
        {
            try
            {
                var result = NotaManager.Update(id, nota);
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

        // DELETE api/<NotaController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Docente")]

        public IActionResult Delete(int id)
        {
            try
            {
                var result = NotaManager.Delete(id);
                if (result == null)
                {
                    return BadRequest("No se encontro registro, vuelva a intentar.");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CargarExcel")]
        [Authorize(Roles = "Administrador,Docente")]

        public IActionResult CargarExcel([FromForm] IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    var result = NotaManager.CargarExcel(filePath);
                    if (result != "")
                    {
                        return BadRequest(result);
                    }
                    return Ok(result);
                }
                else
                {
                    throw new Exception($"No se encontró el archivo adjunto o su tamaño es cero.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExportarNotas")]
        [Authorize(Roles = "Administrador,Docente")]

        public ActionResult ExportarNotas()
        {
            try
            {
                var excelData = NotaManager.ExportarNotas();
                return new FileContentResult(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            }
            catch (Exception ex)
            {
                return BadRequest("Error exportando auditoría");
            }
        }
    }
}
