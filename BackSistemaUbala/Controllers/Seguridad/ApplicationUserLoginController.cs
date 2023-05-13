//ApplicationUserLoginController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using General_back.Security.Models;
using General_back.Security.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BackSistemaUbala.Models.DTOs;

namespace General_back.Seguridad.DataLayer.Models
{
    public class ApplicationUserLoginController : ODataController
    {
        private readonly ILogger<ApplicationUserLoginController> logger;
        private readonly ISeguridadManager seguridadManager;

        public ApplicationUserLoginController(ILogger<ApplicationUserLoginController> logger,
                                    ISeguridadManager seguridadManager)
        {
            this.logger = logger;
            this.seguridadManager = seguridadManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDTO row)
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
