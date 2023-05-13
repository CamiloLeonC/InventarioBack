
using BackSistemaUbala.Models.Enums;
using System.Collections.Generic;

namespace BackSistemaUbala.Models.DTOs
{
    public class UsuarioDTO
    {
        public string Id { get; set; }
        public int? IdGrupo { get; set; }
        public string NombreCompleto { get; set; }
        public Jornada Jornada { get; set; }
        public TipoSangre TipoSangre { get; set; }
        public string Documento { get; set; }
        public string NombreAcudiente { get; set; }
        public string NumeroAcudiente { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string IdRol { get; set; }

    }
}
