using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSistemaUbala.Migrations
{
    public partial class Roles_Usuarios_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Roles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
