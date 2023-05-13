using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BackSistemaUbala.Models.Enums
{
    public enum TipoSangre
    {
        [Description("O +")]
        O_POSITIVO = 1,
        [Description("O -")]
        O_NEGATIVO = 2,
        [Description("A +")]
        A_POSITIVO = 3,
        [Description("A -")]
        A_NEGATIVA = 4,
        [Description("B +")]
        B_POSITIVO = 5,
        [Description("B -")]
        B_NEGATIVO = 6,
        [Description("AB +")]
        AB_POSITIVO = 7,
        [Description("AB -")]
        AB_NEGATIVO = 8

    }
}
