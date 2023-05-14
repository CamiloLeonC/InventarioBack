using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSistemaUbala.Migrations
{
    public partial class RenameIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdSeguimiento",
                table: "Seguimientos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdEquipo",
                table: "Equipos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdEntregaDevolucion",
                table: "EntregasDevoluciones",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdContrato",
                table: "Contratos",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Seguimientos",
                newName: "IdSeguimiento");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Equipos",
                newName: "IdEquipo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EntregasDevoluciones",
                newName: "IdEntregaDevolucion");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contratos",
                newName: "IdContrato");
        }
    }
}
