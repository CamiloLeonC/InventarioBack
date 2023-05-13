using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSistemaUbala.Migrations
{
    public partial class CreacionBaseDeDatosUbaLa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Curso = table.Column<int>(type: "int", nullable: false),
                    Grado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGrupo = table.Column<int>(type: "int", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jornada = table.Column<int>(type: "int", nullable: false),
                    TipoSangre = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreAcudiente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroAcudiente = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alumnos_Grupos_IdGrupo",
                        column: x => x.IdGrupo,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGrupo = table.Column<int>(type: "int", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profesores_Grupos_IdGrupo",
                        column: x => x.IdGrupo,
                        principalTable: "Grupos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MateriaProfesores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProfesor = table.Column<int>(type: "int", nullable: false),
                    IdMateria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaProfesores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriaProfesores_Materias_IdMateria",
                        column: x => x.IdMateria,
                        principalTable: "Materias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MateriaProfesores_Profesores_IdProfesor",
                        column: x => x.IdProfesor,
                        principalTable: "Profesores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMateriaProfesor = table.Column<int>(type: "int", nullable: false),
                    IdAlumno = table.Column<int>(type: "int", nullable: false),
                    Periodo = table.Column<int>(type: "int", nullable: false),
                    NotaSaber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NotaHacer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NotaSer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DefinitivaTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Alumnos_IdAlumno",
                        column: x => x.IdAlumno,
                        principalTable: "Alumnos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notas_MateriaProfesores_IdMateriaProfesor",
                        column: x => x.IdMateriaProfesor,
                        principalTable: "MateriaProfesores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_IdGrupo",
                table: "Alumnos",
                column: "IdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaProfesores_IdMateria",
                table: "MateriaProfesores",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaProfesores_IdProfesor",
                table: "MateriaProfesores",
                column: "IdProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_IdAlumno",
                table: "Notas",
                column: "IdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_IdMateriaProfesor",
                table: "Notas",
                column: "IdMateriaProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_IdGrupo",
                table: "Profesores",
                column: "IdGrupo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "MateriaProfesores");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "Grupos");
        }
    }
}
