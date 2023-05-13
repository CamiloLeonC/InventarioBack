using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSistemaUbala.Migrations
{
    public partial class ActualizacionTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NotaSer",
                table: "Notas",
                type: "decimal(2,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaSaber",
                table: "Notas",
                type: "decimal(2,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaHacer",
                table: "Notas",
                type: "decimal(2,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DefinitivaTotal",
                table: "Notas",
                type: "decimal(2,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NotaSer",
                table: "Notas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaSaber",
                table: "Notas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaHacer",
                table: "Notas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DefinitivaTotal",
                table: "Notas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)");
        }
    }
}
