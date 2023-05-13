using System.ComponentModel;

namespace General_back.Enums.Models
{
    public enum RoleBase
    {
        [Description("Aministrador")]
        Administrator = 1,

        [Description("Docentea")]
        EnterpriseAdministrator = 2,

        [Description("Estudiante - Acudiente")]
        Supervisor = 3
    }
}
