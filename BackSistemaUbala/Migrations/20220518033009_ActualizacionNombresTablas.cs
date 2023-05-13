using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSistemaUbala.Migrations
{
    public partial class ActualizacionNombresTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_Roles_RoleId",
                table: "ApplicationUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_Usuarios_UserId",
                table: "ApplicationUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_MateriaProfesores_Usuarios_IdUser",
                table: "MateriaProfesores");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Usuarios_IdUser",
                table: "Notas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.CreateTable(
                name: "ApplicationRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdGrupo = table.Column<int>(type: "int", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jornada = table.Column<int>(type: "int", nullable: false),
                    TipoSangre = table.Column<int>(type: "int", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreAcudiente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroAcudiente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Grupos_IdGrupo",
                        column: x => x.IdGrupo,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_IdGrupo",
                table: "ApplicationUsers",
                column: "IdGrupo");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRole_ApplicationRole_RoleId",
                table: "ApplicationUserRole",
                column: "RoleId",
                principalTable: "ApplicationRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRole_ApplicationUsers_UserId",
                table: "ApplicationUserRole",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaProfesores_ApplicationUsers_IdUser",
                table: "MateriaProfesores",
                column: "IdUser",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_ApplicationUsers_IdUser",
                table: "Notas",
                column: "IdUser",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_ApplicationRole_RoleId",
                table: "ApplicationUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_ApplicationUsers_UserId",
                table: "ApplicationUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_MateriaProfesores_ApplicationUsers_IdUser",
                table: "MateriaProfesores");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_ApplicationUsers_IdUser",
                table: "Notas");

            migrationBuilder.DropTable(
                name: "ApplicationRole");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IdGrupo = table.Column<int>(type: "int", nullable: false),
                    Jornada = table.Column<int>(type: "int", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NombreAcudiente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroAcudiente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoSangre = table.Column<int>(type: "int", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Grupos_IdGrupo",
                        column: x => x.IdGrupo,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdGrupo",
                table: "Usuarios",
                column: "IdGrupo");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRole_Roles_RoleId",
                table: "ApplicationUserRole",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRole_Usuarios_UserId",
                table: "ApplicationUserRole",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
        }
    }
}
