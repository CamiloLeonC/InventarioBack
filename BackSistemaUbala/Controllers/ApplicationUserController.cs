using BackSistemaUbala.Interfaces;
using BackSistemaUbala.Models;
using BackSistemaUbala.Models.DTOs;
using BackSistemaUbala.Models.InteractionResult;
using General_back.Security.Interfaces;
using General_back.Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackSistemaUbala.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Administrator")]
    //[Authorize(Roles = "Docente")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ILogger<ApplicationUserController> logger;
        private readonly IApplicationUserManager UsuarioManager;
        private readonly ISeguridadManager seguridadManager;
        private readonly string userId;

        public ApplicationUserController(ILogger<ApplicationUserController> logger,
                                    IApplicationUserManager usuariodManager,
                                    ISeguridadManager seguridadManager,
                                    IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.UsuarioManager = usuariodManager;
            this.seguridadManager = seguridadManager;
            this.userId = ApplicationUserTokenHelper.GetIdFromAuthorization(httpContextAccessor.HttpContext.Request);
        }


        // GET: api/<UsuarioController>
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult Get()
        {
            try
            {
                var listUsuario = UsuarioManager.GetAll();
                return Ok(listUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Docente, Alumno")]

        public IActionResult GetById(string id)
        {

            try
            {
                var idUsuario = UsuarioManager.GetById(id);
                return Ok(idUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // POST api/<UsuarioController>
        [HttpPost]
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Post([FromBody] UsuarioDTO Usuario)
        {
            try
            {
                var result = await this.UsuarioManager.Add(Usuario);

                if (result.Success)
                    return Ok(result);
                else
                {
                    logger.Log(LogLevel.Information, $"Inicio de consumo del API: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
                    if (string.IsNullOrEmpty(result.Error))
                        return BadRequest(result.Message);
                    else
                        return BadRequest(result.Error);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Put(string id, [FromBody] UsuarioDTO Usuario)
        {
            try
            {
                var result = UsuarioManager.Update(id, Usuario);
                if (result != null)
                {
                    return NoContent();
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

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Delete(string id)
        {
            try
            {
                if(this.userId == id)
                {
                    return BadRequest("No se puede eliminar el registro, esta logeado.");
                }
                else
                {
                    var result = UsuarioManager.Delete(id);
                    if (result == null)
                    {
                        return BadRequest("No se encontro registro, vuelva a intentar.");
                    }
                    return Ok();
                }                
            }
            catch (Exception ex)
            {
                return BadRequest("El registro se encuentra en uso.");
            }
        }

        [HttpPost("/api/Usuario/Login")]
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Login([FromBody] LoginDTO row)
        {
            var result = await this.seguridadManager.Login(row.UserName, row.Password);
            if (result.Success)
            {
                logger.Log(LogLevel.Information, $"Inicio de sesión de {row.UserName} exitoso");
                return Ok(result);
            }
            else
            {
                logger.Log(LogLevel.Error, $"Error de iniciio de sesión de usuario {row.UserName}");
                return BadRequest(result);
            }
        }
    }

}
