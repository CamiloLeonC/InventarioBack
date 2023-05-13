using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSistemaUbala.Migrations
{
    public partial class Roles_Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Usuarios",
                newName: "NumeroAcudiente");

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdGrupo",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Jornada",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NombreAcudiente",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoSangre",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdAlumno",
                table: "Notas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "Notas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdProfesor",
                table: "MateriaProfesores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "MateriaProfesores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcíon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdGrupo",
                table: "Usuarios",
                column: "IdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_IdUser",
                table: "Notas",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaProfesores_IdUser",
                table: "MateriaProfesores",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_RoleId",
                table: "ApplicationUserRole",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaProfesores_Usuarios_IdUser",
                table: "MateriaProfesores",
                column: "IdUser",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Usuarios_IdUser",
                table: "Notas",
                column: "IdUser",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Grupos_IdGrupo",
                table: "Usuarios",
                column: "IdGrupo",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MateriaProfesores_Usuarios_IdUser",
                table: "MateriaProfesores");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Usuarios_IdUser",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Grupos_IdGrupo",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "ApplicationUserRole");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdGrupo",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Notas_IdUser",
                table: "Notas");

            migrationBuilder.DropIndex(
                name: "IX_MateriaProfesores_IdUser",
                table: "MateriaProfesores");

            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdGrupo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Jornada",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "NombreAcudiente",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TipoSangre",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "MateriaProfesores");

            migrationBuilder.RenameColumn(
                name: "NumeroAcudiente",
                table: "Usuarios",
                newName: "Nombre");

            migrationBuilder.AlterColumn<int>(
                name: "IdAlumno",
                table: "Notas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdProfesor",
                table: "MateriaProfesores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
