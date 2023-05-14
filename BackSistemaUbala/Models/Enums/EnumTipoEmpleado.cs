using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Models.Enums
{
    public enum TipoEmpleado
    {
        [Description("Empleado")]
        Empleado = 1,
        [Description("AuxiliarBodega")]
        AuxiliarBodega,
        [Description("Admin")]
        Admin,
    }
}
