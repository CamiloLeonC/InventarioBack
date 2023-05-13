using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Models.Enums
{
    public enum Periodo
    {
        [Description("Primer Periodo")]
        Primer = 1,

        [Description("Segundo Periodo")]
        Segundo = 2,

        [Description("Tercer Periodo")]
        Tercer = 3,

        [Description("Cuarto Periodo")]
        Cuarto = 4
    }
}
