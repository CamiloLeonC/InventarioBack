using Microsoft.EntityFrameworkCore.Migrations;

namespace BackSistemaUbala.Migrations
{
    public partial class ActualizacionNuevasTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_ApplicationRole_RoleId",
                table: "ApplicationUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_ApplicationUsers_UserId",
                table: "ApplicationUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Grupos_IdGrupo",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MateriaProfesores_ApplicationUsers_IdUser",
                table: "MateriaProfesores");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_ApplicationUsers_IdUser",
                table: "Notas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserRole",
                table: "ApplicationUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationRole",
                table: "ApplicationRole");

            migrationBuilder.RenameTable(
                name: "ApplicationUsers",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "ApplicationUserRole",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "ApplicationRole",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsers_IdGrupo",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_IdGrupo");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserRole_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "AspNetRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AspNetClaim",
                columns: table => new
                {
                    IdClaim = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimCode = table.Column<int>(type: "int", nullable: false),
                    ClaimName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimPestanas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetClaim", x => x.IdClaim);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    ClaimPermission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.UniqueConstraint("AK_AspNetRoleClaims_RoleId_ClaimId", x => new { x.RoleId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetClaim_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "AspNetClaim",
                        principalColumn: "IdClaim",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denied = table.Column<bool>(type: "bit", nullable: true),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.UniqueConstraint("AK_AspNetUserClaims_UserId_ClaimId", x => new { x.UserId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetClaim_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "AspNetClaim",
                        principalColumn: "IdClaim",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_ClaimId",
                table: "AspNetRoleClaims",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_ClaimId",
                table: "AspNetUserClaims",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Grupos_IdGrupo",
                table: "AspNetUsers",
                column: "IdGrupo",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaProfesores_AspNetUsers_IdUser",
                table: "MateriaProfesores",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_AspNetUsers_IdUser",
                table: "Notas",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Grupos_IdGrupo",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MateriaProfesores_AspNetUsers_IdUser",
                table: "MateriaProfesores");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_AspNetUsers_IdUser",
                table: "Notas");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "ApplicationUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "ApplicationUserRole");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "ApplicationRole");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_IdGrupo",
                table: "ApplicationUsers",
                newName: "IX_ApplicationUsers_IdGrupo");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "ApplicationUserRole",
                newName: "IX_ApplicationUserRole_RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "ApplicationRole",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApplicationRole",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserRole",
                table: "ApplicationUserRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationRole",
                table: "ApplicationRole",
                column: "Id");

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
                name: "FK_ApplicationUsers_Grupos_IdGrupo",
                table: "ApplicationUsers",
                column: "IdGrupo",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
