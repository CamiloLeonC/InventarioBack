using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSistemaUbala.Migrations
{
    public partial class Correción_atributo_descripcion_Rol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcíon",
                table: "Roles",
                newName: "Descripcion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Roles",
                newName: "Descripcíon");
        }
    }
}
